<template>
  <v-container>
    <v-layout row>
      <menu-mantenimiento></menu-mantenimiento>
    </v-layout>
    <v-layout row>
      <v-flex xs12>
        <v-dialog v-model="dialog" max-width="400px">
          <template v-slot:activator="{ on }">
            <v-btn color="primary" dark v-on="on" class="mb-2">New Item</v-btn>
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
                    <date-picker-textbox
                      label="Fecha Desde"
                      :fecha.sync="editedItem.FechaDesde"
                    ></date-picker-textbox>
                  </v-flex>
                  <v-flex xs12>
                    <date-picker-textbox
                      label="Fecha Hasta"
                      :fecha.sync="editedItem.FechaHasta"
                    ></date-picker-textbox>
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
        <v-data-table
          :headers="headers"
          :items="lista"
          :sort-by="['FechaDesde']"
          :sort-desc="true"
          :items-per-page="10"
        >
          <template slot="item" slot-scope="props">
            <tr>
              <td>{{ props.item.Nombre }}</td>
              <td>{{ props.item.FechaDesde | date }}</td>
              <td>{{ props.item.FechaHasta | date }}</td>
              <td class="justify-center layout px-0">
                <v-btn icon class="mx-0" @click="editItem(props.item)">
                  <v-icon color="teal">edit</v-icon>
                </v-btn>
                <v-btn icon class="mx-0" @click="deleteItem(props.item)">
                  <v-icon color="pink">delete</v-icon>
                </v-btn>
                <v-btn icon class="mx-0" @click="loadDet(props.item)">
                  <v-icon color="blue">system_update_alt</v-icon>
                </v-btn>
              </td>
            </tr>
          </template>
        </v-data-table>
      </v-flex>
    </v-layout>
    <v-divider></v-divider>
    <v-layout row v-if="selectedPresup">
      <v-flex xs12>
        <v-dialog v-model="dialogDet" max-width="400px">
          <template v-slot:activator="{ on }">
            <v-btn color="primary" dark v-on="on" class="mb-2">New Item</v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="headline">{{ formTitle }}</span>
            </v-card-title>
            <v-card-text>
              <v-container>
                <v-layout row wrap>
                  <v-flex xs12>
                    <conceptos-list
                      :selectedValue.sync="editedItemDet.ConceptoId"
                    ></conceptos-list>
                  </v-flex>
                  <v-flex xs12>
                    <v-text-field
                      label="Monto"
                      v-model="editedItemDet.Monto"
                    ></v-text-field>
                  </v-flex>
                </v-layout>
              </v-container>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click.native="closeDet"
                >Cancel</v-btn
              >
              <v-btn color="blue darken-1" text @click.native="saveDet"
                >Save</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="dialogCopy" max-width="500px">
          <template v-slot:activator="{ on }">
            <v-btn color="seconday" dark v-on="on" class="mb-2"
              >Copy from</v-btn
            >
          </template>
          <v-card>
            <v-card-title>
              <span class="headline">Copiar de</span>
            </v-card-title>
            <v-card-text>
              <v-container>
                <v-layout row wrap>
                  <v-flex xs12>
                    <v-select
                      v-model="copyPresupId"
                      v-bind:items="lista"
                      item-value="PresupuestoId"
                      item-text="Nombre"
                      label="Presupuesto"
                      single-line
                      bottom
                    ></v-select>
                  </v-flex>
                </v-layout>
              </v-container>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click.native="closeCopy"
                >Cancel</v-btn
              >
              <v-btn color="blue darken-1" text @click.native="saveCopy"
                >Save</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-data-table
          :headers="headersDet"
          :items="listaDet"
          :options.sync="paginationDet"
        >
          <template slot="item" slot-scope="props">
            <tr>
              <td>
                <concepto-nombre
                  :ConceptoId="props.item.ConceptoId"
                ></concepto-nombre>
              </td>
              <td>{{ props.item.Monto | currency }}</td>
              <td>
                <presup-det-progress
                  :PresupuestoDet="props.item"
                ></presup-det-progress>
              </td>
              <td class="justify-center layout px-0">
                <v-btn icon class="mx-0" @click="editItemDet(props.item)">
                  <v-icon color="teal">edit</v-icon>
                </v-btn>
                <v-btn icon class="mx-0" @click="deleteItemDet(props.item)">
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
import PresupuestosAPI from "../api/presupuestos";
import { mapActions } from "vuex";
import MenuMantenimiento from "./MenuMantenimiento";
import PresupDetProgress from "./PresupDetProgress";

export default {
  components: {
    MenuMantenimiento,
    PresupDetProgress
  },
  data() {
    return {
      lista: [],
      listaDet: [],
      editedIndex: -1,
      editedItem: {
        PresupuestoId: 0,
        Nombre: "",
        FechaDesde: null,
        FechaHasta: null,
        UsuarioId: null
      },
      defaultItem: {
        PresupuestoId: 0,
        Nombre: "",
        FechaDesde: null,
        FechaHasta: null,
        UsuarioId: null
      },
      editedIndexDet: -1,
      editedItemDet: {
        PresupuestoDetId: 0,
        PresupuestoId: 0,
        ConceptoId: 0,
        Monto: 0
      },
      defaultItemDet: {
        PresupuestoDetId: 0,
        PresupuestoId: 0,
        ConceptoId: 0,
        Monto: 0
      },
      selectedPresup: null,
      copyPresupId: null,
      dialog: false,
      dialogDet: false,
      dialogCopy: false,
      headers: [
        { text: "Nombre", value: "Nombre" },
        { text: "FechaDesde", value: "FechaDesde" },
        { text: "FechaHasta", value: "FechaHasta" },
        { text: "Actions", value: "name", sortable: false }
      ],
      headersDet: [
        { text: "Concepto", value: "ConceptoId" },
        { text: "Monto", value: "Monto  `1" },
        { text: "Progreso", value: "name", sortable: false },
        { text: "Actions", value: "name", sortable: false }
      ],
      pagination: {
        sortBy: ["FechaDesde"],
        sortDesc: true,
        itemsPerPage: 10
      },
      paginationDet: {
        sortBy: ["ConceptoId"],
        itemsPerPage: 10
      }
    };
  },
  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "Nuevo Item" : "Editar Item";
    },
    listaConceptos() {
      return this.$store.getters.allConceptos;
    }
  },
  methods: {
    ...mapActions(["setMessage", "setError"]),
    editItem(item) {
      this.editedIndex = this.lista.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },
    deleteItem(item) {
      const index = this.lista.indexOf(item);
      if (confirm("Are you sure you want to delete this item?")) {
        PresupuestosAPI.deleteRecord(item)
          .then(() => {
            this.lista.splice(index, 1);
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
        PresupuestosAPI.updateRecord(record)
          .then(() => {
            Object.assign(this.lista[index], record);
            this.setMessage("Registro salvado exitosamente");
          })
          .catch(error => {
            this.setError(error);
          });
      } else {
        PresupuestosAPI.saveNewRecord(record)
          .then(response => {
            record.PresupuestoId = response.data.PresupuestoId;
            this.lista.push(record);
            this.setMessage("Registro salvado exitosamente");
          })
          .catch(error => {
            this.setError(error);
          });
      }
      this.close();
    },
    loadDet(record) {
      this.listaDet = [];
      this.defaultItemDet.PresupuestoId = record.PresupuestoId;
      this.editedItemDet = Object.assign({}, this.defaultItemDet);
      this.selectedPresup = record; // Object.assign({}, record)
      PresupuestosAPI.getAllDet(record.PresupuestoId)
        .then(response => {
          this.listaDet = response.data;
        })
        .catch(error => {
          this.setError(error);
        });
    },
    editItemDet(item) {
      this.editedIndexDet = this.listaDet.indexOf(item);
      this.editedItemDet = Object.assign({}, item);
      this.dialogDet = true;
    },
    deleteItemDet(item) {
      const index = this.listaDet.indexOf(item);
      if (confirm("Are you sure you want to delete this item?")) {
        PresupuestosAPI.deleteRecordDet(item)
          .then(() => {
            this.listaDet.splice(index, 1);
          })
          .catch(error => {
            this.setError(error);
          });
      }
    },
    closeDet() {
      this.dialogDet = false;
      setTimeout(() => {
        this.editedItemDet = Object.assign({}, this.defaultItemDet);
        this.editedIndexDet = -1;
      }, 300);
    },
    saveDet() {
      let record = Object.assign({}, this.editedItemDet);
      if (this.editedIndexDet > -1) {
        let index = this.editedIndexDet;
        PresupuestosAPI.updateRecordDet(record)
          .then(() => {
            Object.assign(this.listaDet[index], record);
            this.setMessage("Registro salvado exitosamente");
          })
          .catch(error => {
            this.setError(error);
          });
      } else {
        PresupuestosAPI.saveNewRecordDet(record)
          .then(response => {
            record.PresupuestoDetId = response.data.PresupuestoDetId;
            this.listaDet.push(record);
            this.setMessage("Registro salvado exitosamente");
          })
          .catch(error => {
            this.setError(error);
          });
      }
      this.closeDet();
    },
    closeCopy() {
      this.dialogCopy = false;
    },
    saveCopy() {
      // let presuplista.find(item => item.CuentaId === this.CuentaId)
      PresupuestosAPI.getAllDet(this.copyPresupId)
        .then(response => {
          let detOrigen = response.data;
          detOrigen.forEach(item => {
            item.PresupuestoId = this.selectedPresup.PresupuestoId;
            item.PresupuestoDetId = 0;
            PresupuestosAPI.saveNewRecordDet(item)
              .then(response => {
                item.PresupuestoDetId = response.data.PresupuestoDetId;
                this.listaDet.push(item);
              })
              .catch(error => {
                this.setError(error);
              });
          });
          this.dialogCopy = false;
        })
        .catch(error => {
          this.setError(error);
        });
    }
  },
  mounted() {
    PresupuestosAPI.getAllDesc()
      .then(response => {
        this.lista = response.data;
      })
      .catch(error => {
        this.setError(error);
      });
  }
};
</script>
