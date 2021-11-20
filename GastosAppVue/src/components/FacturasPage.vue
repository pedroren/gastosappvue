<template>
  <v-container>
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
                          autofocus
                          ref="Nombre"
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs12>
                        <favoritos-list
                          :selectedValue.sync="editedItem.DescripFrecuenteId"
                        ></favoritos-list>
                      </v-flex>
                      <v-flex xs12>
                        <v-select
                          :items="listFrecuencias"
                          v-model="editedItem.Frecuencia"
                          label="Frecuencia"
                          single-line
                          item-text="FrecuenciaDesc"
                          item-value="Frecuencia"
                        ></v-select>
                      </v-flex>
                      <v-flex xs4>
                        <v-text-field
                          label="Dia"
                          v-model="editedItem.Dia"
                          required
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs8>
                        <v-text-field
                          label="Mes"
                          v-model="editedItem.Mes"
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs6>
                        <v-text-field
                          label="ProxFecha"
                          v-model="editedItem.ProxFecha"
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs6>
                        <v-switch
                          label="Activo?"
                          v-model="editedItem.Activo"
                        ></v-switch>
                      </v-flex>
                      <v-flex xs6>
                        <v-text-field
                          label="UltFecha"
                          v-model="editedItem.UltFecha"
                          readonly
                          disabled
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs6>
                        <v-text-field
                          label="UltMonto"
                          v-model="editedItem.UltMonto"
                          readonly
                          disabled
                        ></v-text-field>
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
          <v-data-table
            :headers="headers"
            :items="getLista"
            :search="search"
            :loading="loading"
            :sort-by="['ProxFecha']"
            :sort-desc="true"
            :items-per-page="20"
          >
            <template slot="item" slot-scope="props">
              <tr>
                <td>{{ props.item.Nombre }}</td>
                <td>{{ props.item.Frecuencia }}</td>
                <td>{{ props.item.Dia }}</td>
                <td>{{ props.item.Mes }}</td>
                <td>{{ props.item.ProxFecha | date }}</td>
                <td>{{ props.item.Activo }}</td>
                <td>{{ props.item.UltFecha | date }}</td>
                <td class="text-xs-right">
                  {{ props.item.UltMonto | currency }}
                </td>
                <td class="justify-center layout px-0">
                  <v-btn icon class="mx-0" @click="editItem(props.item)">
                    <v-icon color="teal">edit</v-icon>
                  </v-btn>
                  <v-btn icon class="mx-0" @click="deleteItem(props.item)">
                    <v-icon color="pink">delete</v-icon>
                  </v-btn>
                  <v-btn icon class="mx-0" @click="payItem(props.item)">
                    <v-icon color="blue">offline_pin</v-icon>
                  </v-btn>
                </td>
              </tr>
            </template>
          </v-data-table>
        </v-card>
        <trans-create-edit
          :showDialog.sync="dialogTrans"
          :facturaId.sync="facturaId"
          v-on:onsave="refresh"
        ></trans-create-edit>
      </v-flex>
    </v-layout>
  </v-container>
</template>
<script>
import FacturasAPI from "../api/facturas";
import { mapActions } from "vuex";
import FavoritosList from "./FavoritosList";
import TransCreateEdit from "./TransCreateEdit";

export default {
  components: {
    FavoritosList,
    TransCreateEdit
  },
  data() {
    return {
      search: "",
      mostrarInactivos: false,
      lista: [],
      editedIndex: -1,
      editedItem: {
        FacturaId: 0,
        Nombre: "",
        DescripFrecuenteId: 0,
        Frecuencia: "M",
        Dia: 0,
        Mes: null,
        ProxFecha: null,
        Activo: true,
        UltFecha: null,
        UltMonto: null,
        UsuarioId: null
      },
      defaultItem: {
        FacturaId: 0,
        Nombre: "",
        DescripFrecuenteId: 0,
        Frecuencia: "M",
        Dia: 1,
        Mes: 0,
        ProxFecha: new Date(),
        Activo: true,
        UltFecha: null,
        UltMonto: 0,
        UsuarioId: 0
      },
      dialog: false,
      headers: [
        { text: "Nombre", value: "Nombre" },
        { text: "Frecuencia", value: "Frecuencia" },
        { text: "Dia", value: "Dia" },
        { text: "Mes", value: "Mes" },
        { text: "ProxFecha", value: "ProxFecha" },
        { text: "Activo", value: "Activo" },
        { text: "UltFecha", value: "UltFecha" },
        { text: "UltMonto", value: "UltMonto", align: "right" },
        { text: "Actions", value: "name", sortable: false }
      ],
      pagination: {
        sortBy: ["ProxFecha"],
        sortDesc: false,
        itemsPerPage: 20
      },
      loading: true,
      facturaId: null,
      dialogTrans: false,
      listFrecuencias: [
        {
          Frecuencia: "M",
          FrecuenciaDesc: "Mensual"
        },
        {
          Frecuencia: "A",
          FrecuenciaDesc: "Anual"
        }
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
    ...mapActions(["setMessage", "setError"]),
    refresh() {
      this.loading = true;
      FacturasAPI.getAll()
        .then(response => {
          this.lista = response.data;
          this.loading = false;
        })
        .catch(error => {
          this.setError(error);
          this.loading = "error";
        });
    },
    editItem(item) {
      this.editedIndex = this.lista.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },
    deleteItem(item) {
      const index = this.lista.indexOf(item);
      if (confirm("Are you sure you want to delete this item?")) {
        FacturasAPI.deleteRecord(item)
          .then(() => {
            this.lista.splice(index, 1);
          })
          .catch(error => {
            this.setError(error);
          });
      }
    },
    payItem(item) {
      this.facturaId = item.FacturaId;
      this.dialogTrans = true;
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
        FacturasAPI.updateRecord(record)
          .then(() => {
            Object.assign(this.lista[index], record);
            this.setMessage("Registro salvado exitosamente");
          })
          .catch(error => {
            this.setError(error);
          });
      } else {
        FacturasAPI.saveNewRecord(record)
          .then(response => {
            record.DescripFrecuenteId = response.data.DescripFrecuenteId;
            this.lista.push(record);
            this.setMessage("Registro salvado exitosamente");
          })
          .catch(error => {
            this.setError(error);
          });
      }
      this.close();
    }
  },
  mounted() {
    this.editedItem = Object.assign({}, this.defaultItem);
    this.refresh();
  },
  watch: {
    dialog(newVal) {
      if (newVal) {
        requestAnimationFrame(() => this.$refs.Nombre.focus());
      }
    },
    "editedItem.Dia": {
      handler: function(newVal) {
        if (newVal) {
          if (this.editedItem.FacturaId === 0) {
            let record = Object.assign({}, this.editedItem);
            FacturasAPI.getProxFechaFact(record)
              .then(response => {
                this.editedItem.ProxFecha = response.data;
              })
              .catch(error => {
                this.setError(error);
              });
          }
        }
      },
      deep: true
    }
  }
};
</script>
