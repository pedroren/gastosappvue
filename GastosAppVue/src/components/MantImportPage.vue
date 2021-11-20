<template>
  <v-container>
    <v-layout row>
      <menu-mantenimiento></menu-mantenimiento>
    </v-layout>
    <v-layout row>
      <v-flex xs12>
        <v-tabs>
          <v-tab>
            Formato Archivos
          </v-tab>
          <v-tab>
            Tags
          </v-tab>
          <v-tab-item>
            <v-layout row>
              <v-flex xs12>
                <v-dialog v-model="dialog" max-width="400px">
                  <v-btn color="primary" dark slot="activator" class="mb-2"
                    >New Item</v-btn
                  >
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
                      <v-btn color="blue darken-1" flat @click.native="close"
                        >Cancel</v-btn
                      >
                      <v-btn color="blue darken-1" flat @click.native="save"
                        >Save</v-btn
                      >
                    </v-card-actions>
                  </v-card>
                </v-dialog>
                <v-data-table :headers="headers" :items="lista">
                  <template slot="items" slot-scope="props">
                    <td>{{ props.item.Nombre }}</td>
                    <td>{{ props.item.SkipRows }}</td>
                    <td>{{ props.item.EsExcel }}</td>
                    <td>{{ props.item.Separador }}</td>
                    <td>{{ props.item.DateFormat }}</td>
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
                  </template>
                </v-data-table>
              </v-flex>
            </v-layout>
            <v-divider></v-divider>
            <v-layout row v-if="selectedMaster">
              <v-flex xs12>
                <v-dialog v-model="dialogDet" max-width="400px">
                  <v-btn color="primary" dark slot="activator" class="mb-2"
                    >New Item</v-btn
                  >
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
                      <v-btn color="blue darken-1" flat @click.native="closeDet"
                        >Cancel</v-btn
                      >
                      <v-btn color="blue darken-1" flat @click.native="saveDet"
                        >Save</v-btn
                      >
                    </v-card-actions>
                  </v-card>
                </v-dialog>
                <v-data-table :headers="headersDet" :items="listaDet">
                  <template slot="items" slot-scope="props">
                    <td>{{ props.item.Nombre }}</td>
                    <td>{{ props.item.Posicion }}</td>
                    <td class="justify-center layout px-0">
                      <v-btn icon class="mx-0" @click="editItemDet(props.item)">
                        <v-icon color="teal">edit</v-icon>
                      </v-btn>
                      <v-btn
                        icon
                        class="mx-0"
                        @click="deleteItemDet(props.item)"
                      >
                        <v-icon color="pink">delete</v-icon>
                      </v-btn>
                    </td>
                  </template>
                </v-data-table>
              </v-flex>
            </v-layout>
          </v-tab-item>
          <v-tab-item>
            <v-dialog v-model="dialogTag" max-width="400px">
              <v-btn color="primary" dark slot="activator" class="mb-2"
                >New Item</v-btn
              >
              <v-card>
                <v-card-title>
                  <span class="headline">{{ formTitle }}</span>
                </v-card-title>
                <v-card-text>
                  <v-container>
                    <v-layout row wrap>
                      <v-flex xs12>
                        <v-text-field
                          label="Tag"
                          v-model="editedItemTag.Tag"
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs12>
                        <conceptos-list
                          :selectedValue.sync="editedItemTag.ConceptoId"
                        ></conceptos-list>
                      </v-flex>
                      <v-flex xs12>
                        <v-text-field
                          label="Descripcion"
                          v-model="editedItemTag.Descripcion"
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs12>
                        <v-text-field
                          label="Orden"
                          v-model.number="editedItemTag.Orden"
                        ></v-text-field>
                      </v-flex>
                    </v-layout>
                  </v-container>
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" flat @click.native="closeTag"
                    >Cancel</v-btn
                  >
                  <v-btn color="blue darken-1" flat @click.native="saveTag"
                    >Save</v-btn
                  >
                </v-card-actions>
              </v-card>
            </v-dialog>
            <v-data-table :headers="headersTag" :items="listaTag">
              <template slot="items" slot-scope="props">
                <td>{{ props.item.Tag }}</td>
                <td>
                  <concepto-nombre
                    :ConceptoId="props.item.ConceptoId"
                  ></concepto-nombre>
                </td>
                <td>{{ props.item.Descripcion }}</td>
                <td>{{ props.item.Orden }}</td>
                <td class="justify-center layout px-0">
                  <v-btn icon class="mx-0" @click="editItemTag(props.item)">
                    <v-icon color="teal">edit</v-icon>
                  </v-btn>
                  <v-btn icon class="mx-0" @click="deleteItemTag(props.item)">
                    <v-icon color="pink">delete</v-icon>
                  </v-btn>
                </td>
              </template>
            </v-data-table>
          </v-tab-item>
        </v-tabs>
      </v-flex>
    </v-layout>
  </v-container>
</template>
<script>
import ImportsAPI from "../api/imports";
import { mapActions } from "vuex";
import MenuMantenimiento from "./MenuMantenimiento";

export default {
  components: {
    MenuMantenimiento
  },
  data() {
    return {
      lista: [],
      listaDet: [],
      listaTag: [],
      editedIndex: -1,
      editedItem: {
        TransImportMapId: 0,
        Nombre: "",
        UsuarioId: null
      },
      defaultItem: {
        TransImportMapId: 0,
        Nombre: "",
        UsuarioId: null
      },
      editedIndexDet: -1,
      editedItemDet: {
        TransImportMapItemId: 0,
        TransImportMapId: 0,
        Nombre: 0,
        Posicion: 0
      },
      defaultItemDet: {
        TransImportMapItemId: 0,
        TransImportMapId: 0,
        Nombre: 0,
        Posicion: 0
      },
      editedIndexTag: -1,
      editedItemTag: {
        TransImportTagId: 0,
        Orden: 99,
        UsuarioId: null
      },
      defaultItemTag: {
        TransImportTagId: 0,
        Orden: 99,
        UsuarioId: null
      },
      selectedMaster: null,
      dialog: false,
      dialogDet: false,
      dialogTag: false,
      headers: [
        { text: "Nombre", value: "Nombre" },
        { text: "SkipRows", value: "SkipRows" },
        { text: "EsExcel", value: "EsExcel" },
        { text: "Separador", value: "Separador" },
        { text: "DateFormat", value: "DateFormat" },
        { text: "Actions", value: "name", sortable: false }
      ],
      headersDet: [
        { text: "Nombre", value: "Nombre" },
        { text: "Posicion", value: "Posicion" },
        { text: "Actions", value: "name", sortable: false }
      ],
      headersTag: [
        { text: "Tag", value: "Tag" },
        { text: "Concepto", value: "ConceptoId" },
        { text: "Descripcion", value: "Descripcion" },
        { text: "Orden", value: "Orden" }
      ]
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
        ImportsAPI.deleteRecord(item)
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
        ImportsAPI.updateRecord(record)
          .then(() => {
            Object.assign(this.lista[index], record);
            this.setMessage("Registro salvado exitosamente");
          })
          .catch(error => {
            this.setError(error);
          });
      } else {
        ImportsAPI.saveNewRecord(record)
          .then(response => {
            record.TransImportMapId = response.data.TransImportMapId;
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
      this.defaultItemDet.TransImportMapId = record.TransImportMapId;
      this.editedItemDet = Object.assign({}, this.defaultItemDet);
      this.selectedMaster = record; // Object.assign({}, record)
      ImportsAPI.getAllDet(record.TransImportMapId)
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
        ImportsAPI.deleteRecordDet(item)
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
        ImportsAPI.updateRecordDet(record)
          .then(() => {
            Object.assign(this.listaDet[index], record);
            this.setMessage("Registro salvado exitosamente");
          })
          .catch(error => {
            this.setError(error);
          });
      } else {
        ImportsAPI.saveNewRecordDet(record)
          .then(response => {
            record.TransImportMapItemId = response.data.TransImportMapItemId;
            this.listaDet.push(record);
            this.setMessage("Registro salvado exitosamente");
          })
          .catch(error => {
            this.setError(error);
          });
      }
      this.closeDet();
    },
    closeTag() {
      this.dialogTag = false;
      setTimeout(() => {
        this.editedItemTag = Object.assign({}, this.defaultItemTag);
        this.editedIndexTag = -1;
      }, 300);
    },
    saveTag() {
      let record = Object.assign({}, this.editedItemTag);
      if (this.editedIndexTag > -1) {
        let index = this.editedIndexTag;
        ImportsAPI.updateRecordTag(record)
          .then(() => {
            Object.assign(this.listaTag[index], record);
            this.setMessage("Registro salvado exitosamente");
          })
          .catch(error => {
            this.setError(error);
          });
      } else {
        ImportsAPI.saveNewRecordTag(record)
          .then(response => {
            record.TransImportTagId = response.data.TransImportTagId;
            this.listaTag.push(record);
            this.setMessage("Registro salvado exitosamente");
          })
          .catch(error => {
            this.setError(error);
          });
      }
      this.closeTag();
    },
    editItemTag(item) {
      this.editedIndexTag = this.listaTag.indexOf(item);
      this.editedItemTag = Object.assign({}, item);
      this.dialogTag = true;
    },
    deleteItemTag(item) {
      const index = this.listaTag.indexOf(item);
      if (confirm("Are you sure you want to delete this item?")) {
        ImportsAPI.deleteRecordTag(item)
          .then(() => {
            this.listaTag.splice(index, 1);
          })
          .catch(error => {
            this.setError(error);
          });
      }
    }
  },
  mounted() {
    ImportsAPI.getAll()
      .then(response => {
        this.lista = response.data;
      })
      .catch(error => {
        this.setError(error);
      });
    ImportsAPI.getAllTag()
      .then(response => {
        this.listaTag = response.data;
      })
      .catch(error => {
        this.setError(error);
      });
  }
};
</script>
