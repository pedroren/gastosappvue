<template>
  <v-container>
    <v-layout row>
      <menu-mantenimiento></menu-mantenimiento>
    </v-layout>
    <v-layout row wrap>
      <v-flex xs12>
        <v-card>
          <v-card-title>
            <v-dialog v-model="dialog" min-width="200px" max-width="500px">
              <template v-slot:activator="{ on }">
                <v-btn color="primary" v-on="on" slot="activator" class="mb-2"
                  >New Item</v-btn
                >
              </template>
              <v-card>
                <v-card-title>
                  <span class="headline">{{ formTitle }}</span>
                </v-card-title>
                <v-card-text>
                  <v-container>
                    <v-layout row wrap>
                      <v-flex xs12>
                        <v-text-field
                          label="Nombre"
                          v-model="editedItem.Nombre"
                          required
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs12>
                        <conceptos-list
                          :selectedValue.sync="editedItem.ConceptoPadreId"
                        ></conceptos-list>
                      </v-flex>
                      <v-flex xs12>
                        <v-switch
                          label="Es Gasto?"
                          v-model="editedItem.EsGasto"
                        ></v-switch>
                      </v-flex>
                      <v-flex xs12>
                        <v-switch
                          label="Activo?"
                          v-model="editedItem.Activo"
                        ></v-switch>
                      </v-flex>
                    </v-layout>
                  </v-container>
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" flat @click.native="close"
                    >Cancel</v-btn
                  >
                  <v-btn color="blue darken-1" flat @click.native="save"
                    >Save</v-btn
                  >
                </v-card-actions>
              </v-card>
            </v-dialog>
            <v-spacer></v-spacer>
            <v-checkbox
              label="Inactivos"
              v-model="mostrarInactivos"
            ></v-checkbox>
            <v-text-field
              append-icon="search"
              label="Search"
              single-line
              hide-details
              v-model="search"
            ></v-text-field>
          </v-card-title>
          <v-data-table :headers="headers" :items="getLista" :search="search">
            <template slot="item" slot-scope="props">
              <tr>
                <td>{{ props.item.ConceptoId }}</td>
                <td>{{ props.item.Nombre }}</td>
                <td>
                  <concepto-nombre
                    :ConceptoId="props.item.ConceptoPadreId"
                  ></concepto-nombre>
                </td>
                <td>{{ props.item.EsGasto ? "Si" : "No" }}</td>
                <td>{{ props.item.Activo ? "Si" : "No" }}</td>
                <td class="justify-center layout px-0">
                  <v-btn icon class="mx-0" @click="editItem(props.item)">
                    <v-icon color="teal">edit</v-icon>
                  </v-btn>
                  <v-btn icon class="mx-0" @click="deleteItem(props.item)">
                    <v-icon color="pink">delete</v-icon>
                  </v-btn>
                </td>
              </tr>
            </template>
          </v-data-table>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>
<script>
import ConceptosAPI from "../api/conceptos";
import { mapActions } from "vuex";
import MenuMantenimiento from "./MenuMantenimiento";

export default {
  components: {
    MenuMantenimiento
  },
  data() {
    return {
      search: "",
      mostrarInactivos: false,
      lista: [],
      editedIndex: -1,
      editedItem: {
        ConceptoId: 0,
        Nombre: "",
        ConceptoPadreId: null,
        EsGasto: true,
        Activo: true,
        UsuarioId: null
      },
      defaultItem: {
        ConceptoId: 0,
        Nombre: "",
        ConceptoPadreId: null,
        EsGasto: true,
        Activo: true,
        UsuarioId: null
      },
      dialog: false,
      headers: [
        { text: "Id", value: "ConceptoId" },
        { text: "Nombre", value: "Nombre" },
        { text: "Padre", value: "ConceptoPadreId" },
        { text: "Es Gasto", value: "EsGasto" },
        { text: "Activo", value: "Activo" },
        { text: "Actions", value: "name", sortable: false }
      ]
    };
  },
  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "Nuevo Item" : "Editar Item";
    },
    getLista() {
      if (this.mostrarInactivos) {
        return this.lista;
      } else {
        return this.lista.filter(item => {
          return item.Activo;
        });
      }
    }
  },
  methods: {
    ...mapActions(["refreshConceptos", "setMessage", "setError"]),
    editItem(item) {
      this.editedIndex = this.lista.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },
    deleteItem(item) {
      const index = this.lista.indexOf(item);
      if (confirm("Are you sure you want to delete this item?")) {
        ConceptosAPI.deleteRecord(item)
          .then(() => {
            this.lista.splice(index, 1);
            this.refreshConceptos();
          })
          .catch(error => {
            this.setError(error);
          });
      }
    },
    close() {
      this.dialog = false;
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      }, 300);
    },
    save() {
      let record = Object.assign({}, this.editedItem);
      if (this.editedIndex > -1) {
        let index = this.editedIndex;
        ConceptosAPI.updateRecord(record)
          .then(() => {
            Object.assign(this.lista[index], record);
            this.setMessage("Registro salvado exitosamente");
            this.refreshConceptos();
            this.refreshStore();
          })
          .catch(error => {
            this.setError(error);
          });
      } else {
        ConceptosAPI.saveNewRecord(record)
          .then(response => {
            record.ConceptoId = response.data.ConceptoId;
            this.lista.push(record);
            this.setMessage("Registro salvado exitosamente");
            this.refreshConceptos();
            this.refreshStore();
          })
          .catch(error => {
            this.setError(error);
          });
      }
      this.close();
    },
    refreshStore() {
      this.$store.dispatch("refreshConceptos");
    }
  },
  mounted() {
    ConceptosAPI.getAll()
      .then(response => {
        this.lista = response.data;
      })
      .catch(error => {
        this.setError(error);
      });
  }
};
</script>
