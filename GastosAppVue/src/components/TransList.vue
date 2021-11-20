<template>
  <v-dialog v-model="dialog" min-width="200px" max-width="500px">
    <v-card>
      <v-card-text>
        <v-data-table
          :headers="headers"
          :items="lista"
          :loading="loading"
          item-key="TransaccionId"
          show-expand
        >
          <template v-slot:item.Fecha="{ item }">
            {{ item.Fecha | date }}
          </template>
          <template v-slot:item.Monto="{ item }">
            <v-tooltip bottom>
              <template v-slot:activator="{ on }">
                <span v-on="on">{{ item.MontoEquivalente | currency }}</span>
              </template>
              <span>{{ item.AbreviaturaMoneda }}{{ item.Monto }}</span>
            </v-tooltip>
          </template>
          <template v-slot:expanded-item="{ headers, item }">
            <td :colspan="headers.length">
              {{ item.Comentario }}
            </td>
          </template>
        </v-data-table>
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script>
import { mapActions } from "vuex";
import TransaccionesAPI from "../api/transacciones";

export default {
  props: {
    filtroFechaDesde: String,
    filtroFechaHasta: String,
    filtroConcepto: Number,
    filtroCuenta: Number,
    showDialog: Boolean
  },
  data() {
    return {
      dialog: false,
      lista: [],
      headers: [
        { text: "Descripcion", value: "Descripcion", sortable: false },
        { text: "Fecha", value: "Fecha" },
        { text: "Monto", value: "Monto", align: "right" }
      ],
      loading: false
    };
  },
  computed: {
    formTitle() {
      return "Detalle";
    }
  },
  methods: {
    ...mapActions(["setMessage", "setError"]),
    close() {
      this.dialog = false;
    }
  },
  watch: {
    showDialog(newVal) {
      this.dialog = newVal;
      if (newVal) {
        TransaccionesAPI.getData(
          this.filtroFechaDesde,
          this.filtroFechaHasta,
          this.filtroConcepto,
          this.filtroCuenta
        )
          .then(response => {
            this.lista = response.data;
            this.loading = false;
          })
          .catch(error => {
            this.setError(error);
            this.loading = "error";
          });
      }
    },
    dialog(newVal) {
      this.$emit("update:showDialog", newVal);
    }
  }
};
</script>

<style></style>
