<template>
  <v-dialog v-model="dialog" min-width="200px" max-width="500px">
    <v-card>
      <v-card-text>
        <v-container>
          <v-layout row wrap v-if="dialog">
            <v-flex xs12>
              <v-radio-group row v-model="editedItem.TipoTransaccionId">
                <v-radio
                  v-for="item in allTiposTrans"
                  :key="item.TipoTransaccionId"
                  :label="item.Nombre"
                  :value="item.TipoTransaccionId"
                ></v-radio>
              </v-radio-group>
            </v-flex>
            <v-flex xs12>
              <v-combobox
                v-model="editedItem.Descripcion"
                :items="listaFavoritos"
                @input="OnInputFav"
                item-value="Nombre"
                item-text="Nombre"
                label="Descripcion"
                ref="Descripcion"
                required
                autofocus
                :error="!descripIsValid"
              ></v-combobox>
              <!-- <vue-suggest
                :list="getFavFiltered"
                value-attribute="DescripFrecuenteId"
                display-attribute="Nombre"
                @select="favSelected"
                mode="input"
                destyled
                :min-length="1"
                :filter-by-query="true"
                :controls="{
                  selectionUp: [38, 33],
                  selectionDown: [40, 34],
                  select: [13, 36],
                  hideList: [27, 35],
                  autocomplete: [32, 13],
                }"> -->
              <!-- Filter by query to only show the filtered results -->
              <!-- v-model on input itself is useless -->
              <!-- <v-text-field label="Descripcion" ref="Descripcion" v-model="editedItem.Descripcion" required autofocus></v-text-field> -->
              <!-- Appears o top of the list -->
              <!-- <v-chip outline label color="primary" slot="suggestion-item" slot-scope="{ suggestion }">
                  <v-icon left>label</v-icon>{{ suggestion.Nombre }}
                </v-chip> -->
              <!-- Appears below the list -->
              <!-- <v-footer slot="misc-item-below" slot-scope="{ suggestion }">
                </v-footer> -->
              <!-- </vue-suggest> -->
            </v-flex>
            <v-flex xs6>
              <date-picker-textbox
                label="Fecha"
                :fecha.sync="editedItem.Fecha"
              ></date-picker-textbox>
            </v-flex>
            <v-flex xs5 offset-xs1>
              <v-text-field
                label="Monto"
                v-model.number="editedItem.Monto"
                required
                ref="monto"
                type="number"
                :error="!montoIsValid"
                prefix="$"
              ></v-text-field>
            </v-flex>
            <v-flex xs12 v-if="!esTransferencia">
              <conceptos-list
                :selectedValue.sync="editedItem.ConceptoId"
              ></conceptos-list>
            </v-flex>
            <v-flex xs12>
              <cuentas-list
                :selectedValue.sync="editedItem.CuentaId"
              ></cuentas-list>
            </v-flex>
            <v-flex xs12 v-if="esTransferencia">
              <cuentas-list
                label="Cuenta Transf."
                :selectedValue.sync="editedItem.CuentaIdTransf"
              ></cuentas-list>
            </v-flex>
            <v-flex xs5 v-if="esTransferencia">
              <v-text-field
                label="Tasa"
                v-model="TasaTransfCalc"
                readonly
                disabled
              ></v-text-field>
            </v-flex>
            <v-flex xs6 offset-xs1 v-if="esTransferencia">
              <v-text-field
                label="MontoTransf"
                v-model.number="MontoTransfCalc"
                readonly
                disabled
              ></v-text-field>
            </v-flex>
            <v-flex xs12>
              <v-textarea label="Comentario" v-model="editedItem.Comentario">
              </v-textarea>
            </v-flex>
          </v-layout>
        </v-container>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="blue darken-1" text @click.native="close">Cancel</v-btn>
        <v-btn
          color="blue darken-1"
          text
          @click.native="save"
          :disabled="!formIsValid"
          >Save</v-btn
        >
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import TransaccionesAPI from "../api/transacciones";
import FacturasAPI from "../api/facturas";
import FavoritosAPI from "../api/favoritos";
// import VueSuggest from 'vue-simple-suggest'

export default {
  props: [
    "transaccionId",
    "facturaId",
    "showDialog",
    "returnRecord",
    "contextDate",
    "contextCuenta",
    "contextConcepto"
  ],
  data() {
    return {
      dialog: false,
      saving: false,
      editedItem: {
        TransaccionId: 0,
        TipoTransaccionId: 0,
        Descripcion: "",
        Fecha: null,
        ConceptoId: null,
        Monto: 0,
        CuentaId: null,
        CuentaIdTransf: null,
        TasaTransf: 1,
        MontoTransf: 0,
        UsuarioId: null,
        FacturaId: null
      },
      listaFavoritos: []
    };
  },
  computed: {
    ...mapGetters({
      defaultItem: "newTransTemplate",
      allTiposTrans: "allTiposTrans",
      todayDateString: "todayDateString"
    }),
    montoIsValid() {
      return this.editedItem.Monto && this.editedItem.Monto !== 0.0;
    },
    descripIsValid() {
      return this.editedItem.Descripcion && this.editedItem.Descripcion !== "";
    },
    formIsValid() {
      return this.descripIsValid && this.montoIsValid && !this.saving;
    },
    fechaDate() {
      return new Date(this.editedItem.Fecha);
    },
    esTransferencia() {
      return this.editedItem.TipoTransaccionId === 2;
    },
    formTitle() {
      return this.editedItem.TransaccionId > 0 ? "Editar Item" : "Nuevo Item";
    },
    TasaTransfCalc() {
      if (this.editedItem.CuentaIdTransf) {
        var cuentaTransf = this.$store.getters.allCuentas.find(
          item => item.CuentaId === this.editedItem.CuentaIdTransf
        );
        return cuentaTransf.Moneda.Tasa;
      } else {
        console.log("no hay CuentaIdTransf");
        // this.editedItem.TasaTransf = 1
        return 1;
      }
    },
    MontoTransfCalc() {
      if (this.TasaTransfCalc) {
        return (this.editedItem.Monto / this.TasaTransfCalc).toFixed(2);
      } else {
        return 0;
      }
    }
  },
  methods: {
    ...mapActions(["setMessage", "setError"]),
    close() {
      this.saving = false;
      this.dialog = false;
    },
    saveOk(record) {
      this.setMessage("Registro salvado exitosamente");
      this.$emit("update:returnRecord", record);
      this.$emit("onsave");
      this.saving = false;
      this.close();
    },
    save() {
      this.saving = true;
      this.editedItem.MontoTransf = this.MontoTransfCalc;
      let record = Object.assign({}, this.editedItem);
      if (typeof record.Descripcion === "object") {
        record.Descripcion = record.Nombre;
      }
      if (this.facturaId) {
        record.FacturaId = this.facturaId;
      }
      if (record.TransaccionId) {
        // let index = this.editedIndex
        TransaccionesAPI.updateRecord(record)
          .then(response => {
            // Object.assign(this.lista[index], record)
            record.TransaccionId = response.data.TransaccionId;
            this.saveOk(record);
          })
          .catch(error => {
            this.saving = false;
            this.setError(error);
          });
      } else {
        TransaccionesAPI.saveNewRecord(record)
          .then(response => {
            record.TransaccionId = response.data.TransaccionId;
            // this.lista.push(record)
            this.saveOk(record);
          })
          .catch(error => {
            this.saving = false;
            this.setError(error);
          });
      }
    },
    cancel() {
      this.$emit("oncancel");
    },
    getFavFiltered(query) {
      return this.listaFavoritos.filter(f => {
        return f.Nombre.toLowerCase().includes(query.toLowerCase());
      });
    },
    favSelected(item) {
      console.log(item);
      if (item) {
        // let favorito = item.selectedObject
        let favorito = item;
        this.editedItem.Descripcion = favorito.Nombre;
        this.editedItem.Monto = favorito.Monto;
        console.log(favorito.CuentaIdTransf);
        if (favorito.CuentaIdTransf) {
          this.editedItem.TipoTransaccionId = 2;
          this.editedItem.CuentaIdTransf = favorito.CuentaIdTransf;
        } else {
          this.editedItem.TipoTransaccionId = 1;
          this.editedItem.CuentaIdTransf = null;
        }
        console.log(favorito.ConceptoId);
        if (favorito.ConceptoId) {
          this.editedItem.ConceptoId = favorito.ConceptoId;
        } else {
          this.editedItem.ConceptoId = null;
        }
        console.log(favorito.CuentaId);
        if (favorito.CuentaId) {
          this.editedItem.CuentaId = favorito.CuentaId;
        } else {
          this.editedItem.CuentaId = null;
        }
        this.$refs.monto.focus();
      }
    },
    OnInputFav() {
      console.log(this.editedItem.Descripcion);
      if (this.editedItem.Descripcion != null) {
        if (typeof this.editedItem.Descripcion === "object") {
          this.favSelected(this.editedItem.Descripcion);
        }
      }
    }
  },
  mounted() {
    this.editedItem = Object.assign({}, this.defaultItem);
    FavoritosAPI.getAll().then(response => {
      this.listaFavoritos = response.data.filter(f => {
        return f.Activo;
      });
    });
  },
  watch: {
    showDialog(newVal) {
      if (newVal) {
        if (this.transaccionId) {
          // Modificando registro
          TransaccionesAPI.getDataById(this.transaccionId)
            .then(response => {
              this.editedItem = response.data;
            })
            .catch(error => {
              this.setError(error);
            });
        } else {
          // Nuevo registro
          this.editedItem = Object.assign({}, this.defaultItem);
          this.editedItem.Fecha = this.todayDateString;
          if (this.facturaId) {
            // Para pagar un factura
            FacturasAPI.getDataById(this.facturaId).then(response => {
              let factura = response.data;
              this.editedItem.Descripcion = factura.Nombre;
              // Buscar información del Favorito
              // var descripFrecuente = this.currentFactura.DescripFrecuente
              FavoritosAPI.getDataById(factura.DescripFrecuenteId).then(
                response => {
                  let favorito = response.data;
                  this.editedItem.ConceptoId = favorito.ConceptoId;
                  this.editedItem.CuentaId = favorito.CuentaId;
                  if (factura.UltMonto > 0) {
                    this.editedItem.Monto = factura.UltMonto;
                  } else {
                    this.editedItem.Monto = favorito.Monto;
                  }
                  if (favorito.CuentaIdTransf) {
                    this.editedItem.TipoTransaccionId = 2;
                    this.editedItem.CuentaIdTransf = favorito.CuentaIdTransf;
                    this.editedItem.TasaTransf =
                      favorito.CuentaTransf.Moneda.Tasa;
                  } else {
                    this.editedItem.TipoTransaccionId = 1;
                    this.editedItem.CuentaIdTransf = null;
                  }
                  if (this.editedItem.TasaTransf !== 0) {
                    this.editedItem.MontoTransf =
                      this.editedItem.Monto / this.editedItem.TasaTransf;
                  }
                }
              );
            });
          } else {
            if (this.contextDate) {
              this.editedItem.Fecha = this.contextDate;
            }
            if (this.contextCuenta) {
              this.editedItem.CuentaId = this.contextCuenta;
            }
            if (this.contextConcepto) {
              this.editedItem.ConceptoId = this.contextConcepto;
            }
          }
        }
        requestAnimationFrame(() => this.$refs.Descripcion.focus());
      }
      this.dialog = newVal;
    },
    dialog(newVal) {
      this.$emit("update:showDialog", newVal);
      if (!newVal) {
        // Limpia variables para preparar componente para registro nuevo
        setTimeout(() => {
          this.editedItem = Object.assign({}, this.defaultItem);
          this.$emit("update:transaccionId", 0);
        }, 300);
      }
    }
    /*'editedItem.Monto' (newVal) {
      if (newVal){
        if (newVal.con) {
          newVal = parseFloat(newVal.replace(/,/g, ''))
        }
      }
    }*/
  }
};
</script>

<style>
.vue-simple-suggest .suggestions .suggest-item.hover {
  background-color: rgb(172, 205, 248) !important;
}
</style>
