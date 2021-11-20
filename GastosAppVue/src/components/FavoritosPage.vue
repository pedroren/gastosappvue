<template>
  <v-container>
    <v-layout row>
      <menu-mantenimiento></menu-mantenimiento>
    </v-layout>
    <v-layout row>
      <v-flex xs12>
        <v-card>
          <v-card-title>
            <v-dialog v-model="dialog" min-width="200px" max-width="500px">
              <template v-slot:activator="{ on }">
                <v-btn color="primary" dark v-on="on" class="mb-2"
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
                          :selectedValue.sync="editedItem.ConceptoId"
                        ></conceptos-list>
                      </v-flex>
                      <v-flex xs12>
                        <v-text-field
                          label="Monto"
                          v-model="editedItem.Monto"
                          required
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs12>
                        <cuentas-list
                          :selectedValue.sync="editedItem.CuentaId"
                        ></cuentas-list>
                      </v-flex>
                      <v-flex xs12>
                        <cuentas-list
                          label="Cuenta Transf."
                          :selectedValue.sync="editedItem.CuentaIdTransf"
                        ></cuentas-list>
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
                  <v-btn color="blue darken-1" text @click.native="close"
                    >Cancel</v-btn
                  >
                  <v-btn color="blue darken-1" text @click.native="save"
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
                <td>{{ props.item.DescripFrecuenteId }}</td>
                <td>{{ props.item.Nombre }}</td>
                <td>
                  <concepto-nombre
                    :ConceptoId="props.item.ConceptoId"
                  ></concepto-nombre>
                </td>
                <td class="text-xs-right">{{ props.item.Monto | currency }}</td>
                <td>
                  <cuenta-nombre
                    :CuentaId="props.item.CuentaId"
                  ></cuenta-nombre>
                </td>
                <td>
                  <cuenta-nombre
                    :CuentaId="props.item.CuentaIdTransf"
                  ></cuenta-nombre>
                </td>
                <td>{{ props.item.Activo }}</td>
                <td class="justify-center layout px-0">
                  <v-btn icon class="mx-0" @click="editItem(props.item)">
                    <v-icon color="teal">edit</v-icon>
                  </v-btn>
                  <v-btn icon class="mx-0" @click="deleteItem(props.item)">
                    <v-icon color="pink">delete</v-icon>
                  </v-btn>
                  <v-btn icon class="mx-0" @click="disableItem(props.item)">
                    <v-icon color="blue">switch_camera</v-icon>
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
import FavoritosAPI from "../api/favoritos";
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
        DescripFrecuenteId: 0,
        Nombre: "",
        ConceptoId: null,
        Monto: 0,
        CuentaId: null,
        CuentaIdTransf: null,
        Activo: true,
        UsuarioId: null
      },
      defaultItem: {
        DescripFrecuenteId: 0,
        Nombre: "",
        ConceptoId: null,
        Monto: 0,
        CuentaId: null,
        CuentaIdTransf: null,
        Activo: true,
        UsuarioId: null
      },
      dialog: false,
      headers: [
        { text: "Id", value: "DescripFrecuenteId" },
        { text: "Nombre", value: "Nombre" },
        { text: "Concepto", value: "ConceptoId" },
        { text: "Monto", value: "Monto", align: "right" },
        { text: "Cuenta", value: "CuentaId" },
        { text: "Cuenta Transf", value: "CuentaIdTransf" },
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
    ...mapActions(["refreshFavoritos", "setMessage", "setError"]),
    editItem(item) {
      this.editedIndex = this.lista.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },
    deleteItem(item) {
      const index = this.lista.indexOf(item);
      if (confirm("Are you sure you want to delete this item?")) {
        FavoritosAPI.deleteRecord(item)
          .then(() => {
            this.lista.splice(index, 1);
            this.refreshFavoritos();
          })
          .catch(error => {
            this.setError(error);
          });
      }
    },
    disableItem(item) {
      if (confirm("Are you sure you want to disable/enable this item?")) {
        FavoritosAPI.disableRecord(item)
          .then(response => {
            item.Activo = response.data.Activo;
            this.refreshFavoritos();
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
        FavoritosAPI.updateRecord(record)
          .then(() => {
            Object.assign(this.lista[index], record);
            this.setMessage("Registro salvado exitosamente");
            this.refreshFavoritos();
          })
          .catch(error => {
            this.setError(error);
          });
      } else {
        FavoritosAPI.saveNewRecord(record)
          .then(response => {
            record.DescripFrecuenteId = response.data.DescripFrecuenteId;
            this.lista.push(record);
            this.setMessage("Registro salvado exitosamente");
            this.refreshFavoritos();
          })
          .catch(error => {
            this.setError(error);
          });
      }
      this.close();
    }
  },
  mounted() {
    FavoritosAPI.getAll()
      .then(response => {
        this.lista = response.data;
      })
      .catch(error => {
        this.setError(error);
      });
  }
};
</script>
