<template>
  <v-container>
    <v-layout row>
      <menu-mantenimiento></menu-mantenimiento>
    </v-layout>
    <v-layout row>
      <v-flex xs12>
        <v-dialog v-model="dialog" min-width="200px" max-width="500px">
          <template v-slot:activator="{ on }">
            <v-btn color="primary" dark v-on="on" class="mb-2">New Item</v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="headline">{{ formTitle }}</span>
            </v-card-title>
            <v-card-text>
              <v-container>
                <v-layout row justify-center>
                  <v-form>
                    <v-flex xs12>
                      <v-text-field
                        v-model="editedItem.MonedaId"
                        hidden
                      ></v-text-field>
                    </v-flex>
                    <v-flex xs12>
                      <v-text-field
                        label="Nombre"
                        v-model="editedItem.Nombre"
                        required
                        ref="editedItemNombre"
                      ></v-text-field>
                    </v-flex>
                    <v-flex xs12>
                      <v-text-field
                        label="Abreviatura"
                        v-model="editedItem.Abreviatura"
                      ></v-text-field>
                    </v-flex>
                    <v-flex xs12>
                      <v-text-field
                        label="Tasa"
                        v-model.number="editedItem.Tasa"
                        required
                      ></v-text-field>
                    </v-flex>
                    <v-flex xs12>
                      <v-switch
                        label="EsPrincipal?"
                        v-model="editedItem.EsPrincipal"
                      ></v-switch>
                    </v-flex>
                  </v-form>
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
        <v-data-table :headers="headers" :items="lista">
          <template slot="item" slot-scope="props">
            <tr>
              <td>{{ props.item.MonedaId }}</td>
              <td>{{ props.item.Nombre }}</td>
              <td>{{ props.item.Abreviatura }}</td>
              <td>{{ props.item.Tasa }}</td>
              <td>{{ props.item.EsPrincipal ? "Si" : "No" }}</td>
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
      </v-flex>
    </v-layout>
  </v-container>
</template>
<script>
import MonedasAPI from "../api/monedas";
import { mapActions } from "vuex";
import MenuMantenimiento from "./MenuMantenimiento";

export default {
  components: {
    MenuMantenimiento
  },
  data() {
    return {
      lista: [],
      editedIndex: -1,
      editedItem: {
        MonedaId: 0,
        Nombre: "",
        Abreviatura: "$",
        Tasa: 1.0,
        EsPrincipal: false,
        UsuarioId: null
      },
      defaultItem: {
        MonedaId: 0,
        Nombre: "",
        Abreviatura: "$",
        Tasa: 1.0,
        EsPrincipal: false,
        UsuarioId: null
      },
      dialog: false,
      headers: [
        { text: "Id", value: "MonedaId" },
        { text: "Nombre", value: "Nombre" },
        { text: "Abrev.", value: "Abreviatura" },
        { text: "Tasa", value: "Tasa" },
        { text: "Principal?", value: "EsPrincipal" },
        { text: "Actions", value: "name", sortable: false }
      ]
    };
  },
  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "Nuevo Item" : "Editar Item";
    }
  },
  methods: {
    ...mapActions(["refreshMonedas", "setMessage", "setError"]),
    editItem(item) {
      this.editedIndex = this.lista.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },
    deleteItem(item) {
      const index = this.lista.indexOf(item);
      if (confirm("Are you sure you want to delete this item?")) {
        MonedasAPI.deleteRecord(item)
          .then(() => {
            this.lista.splice(index, 1);
            this.refreshMonedas();
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
        MonedasAPI.updateRecord(record)
          .then(() => {
            Object.assign(this.lista[index], record);
            this.setMessage("Registro salvado exitosamente");
            this.refreshMonedas();
            this.refreshStore();
          })
          .catch(error => {
            this.setError(error);
          });
      } else {
        MonedasAPI.saveNewRecord(record)
          .then(response => {
            record.MonedaId = response.data.MonedaId;
            this.lista.push(record);
            this.setMessage("Registro salvado exitosamente");
            this.refreshMonedas();
            this.refreshStore();
          })
          .catch(error => {
            this.setError(error);
          });
      }
      this.close();
    },
    refreshStore() {
      this.$store.dispatch("refreshMonedas");
      this.$store.dispatch("refreshCuentas");
    }
  },
  mounted() {
    MonedasAPI.getAll()
      .then(response => {
        this.lista = response.data;
      })
      .catch(error => {
        this.setError(error);
      });
  },
  watch: {
    dialog(val) {
      if (val) {
        requestAnimationFrame(() => {
          this.$refs.editedItemNombre.focus();
        });
      }
    }
  }
};
</script>
