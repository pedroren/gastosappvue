<template>
  <v-container>
    <v-layout row>
      <v-flex xs12>
        <v-card>
          <v-card-title>
            <v-dialog v-model="dialog" max-width="500px">
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
                        <v-text-field
                          label="Balance"
                          v-model="editedItem.Balance"
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs12>
                        <v-radio-group
                          row
                          v-model="editedItem.TipoCuentaId"
                          required
                        >
                          <v-radio
                            v-for="item in listaTipoCuentas"
                            :key="item.TipoCuentaId"
                            :label="item.Nombre"
                            :value="item.TipoCuentaId"
                          ></v-radio>
                        </v-radio-group>
                      </v-flex>
                      <v-flex xs12>
                        <v-select
                          label="Moneda"
                          v-model="editedItem.MonedaId"
                          :items="listaMonedas"
                          item-text="Nombre"
                          item-value="MonedaId"
                          :rules="[v => !!v || 'Item is required']"
                          required
                        ></v-select>
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
                <td>{{ props.item.CuentaId }}</td>
                <td>{{ props.item.Nombre }}</td>
                <td class="text-xs-right">
                  {{ props.item.Balance | currency }}
                </td>
                <td>{{ props.item.Activo ? "Si" : "No" }}</td>
                <td>
                  <tipo-cuenta-nombre
                    :TipoCuentaId="props.item.TipoCuentaId"
                  ></tipo-cuenta-nombre>
                </td>
                <td>
                  <moneda-nombre
                    :MonedaId="props.item.MonedaId"
                  ></moneda-nombre>
                </td>
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
import CuentasAPI from "../api/cuentas";
import { mapActions } from "vuex";

export default {
  data() {
    return {
      search: "",
      mostrarInactivos: false,
      lista: [],
      editedIndex: -1,
      editedItem: {
        CuentaId: 0,
        Nombre: "",
        Balance: 0,
        Activo: true,
        TipoCuentaId: 0,
        MonedaId: 0,
        UsuarioId: null
      },
      defaultItem: {
        CuentaId: 0,
        Nombre: "",
        Balance: 0,
        Activo: true,
        TipoCuentaId: 1,
        MonedaId: 0,
        UsuarioId: null
      },
      dialog: false,
      headers: [
        { text: "Id", value: "CuentaId" },
        { text: "Nombre", value: "Nombre" },
        { text: "Balance", value: "Balance", align: "right" },
        { text: "Activa", value: "Activo" },
        { text: "TipoCuenta", value: "TipoCuentaId", sortable: false },
        { text: "Moneda", value: "MonedaId", sortable: false },
        { text: "Actions", value: "name", sortable: false }
      ]
    };
  },
  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "Nuevo Item" : "Editar Item";
    },
    listaTipoCuentas() {
      return this.$store.getters.allTipoCuentas;
    },
    listaMonedas() {
      return this.$store.getters.allMonedas;
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
    ...mapActions(["refreshCuentas", "setMessage", "setError"]),
    editItem(item) {
      this.editedIndex = this.lista.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },
    deleteItem(item) {
      const index = this.lista.indexOf(item);
      if (confirm("Are you sure you want to delete this item?")) {
        CuentasAPI.deleteRecord(item)
          .then(() => {
            this.lista.splice(index, 1);
            this.refreshCuentas();
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
        CuentasAPI.updateRecord(record)
          .then(() => {
            Object.assign(this.lista[index], record);
            this.refreshCuentas();
            this.setMessage("Registro salvado exitosamente");
            this.refreshStore();
          })
          .catch(error => {
            this.setError(error);
          });
      } else {
        CuentasAPI.saveNewRecord(record)
          .then(response => {
            record.CuentaId = response.data.CuentaId;
            this.lista.push(record);
            this.setMessage("Registro salvado exitosamente");
            this.refreshCuentas();
            this.refreshStore();
          })
          .catch(error => {
            this.setError(error);
          });
      }
      this.close();
    },
    refreshStore() {
      this.$store.dispatch("refreshCuentas");
    }
  },
  mounted() {
    CuentasAPI.getAll()
      .then(response => {
        this.lista = response.data;
      })
      .catch(error => {
        this.setError(error);
      });
  }
};
</script>
