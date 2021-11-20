<template>
  <v-tooltip top>
    <template v-slot:activator="{ on }">
      <v-progress-linear
        :value="valorPorc"
        :color="barColor"
        height="20"
        v-on="on"
      ></v-progress-linear>
    </template>
    <span>{{ valorPorc }}%</span>
  </v-tooltip>
</template>

<script>
import PresupuestosAPI from "../api/presupuestos";
import TransaccionesAPI from "../api/transacciones";

export default {
  props: ["PresupuestoDet"],
  data() {
    return {
      presupuesto: null,
      montoPresup: 0,
      montoEjecutado: 0
    };
  },
  async mounted() {
    let response = await PresupuestosAPI.getById(
      this.PresupuestoDet.PresupuestoId
    );
    this.presupuesto = response.data;
    this.montoPresup = this.PresupuestoDet.Monto;
    let response2 = await TransaccionesAPI.getTotalConcepto(
      this.presupuesto.FechaDesde,
      this.presupuesto.FechaHasta,
      this.PresupuestoDet.ConceptoId
    );
    this.montoEjecutado = response2.data * -1;
  },
  computed: {
    valorPorc() {
      if (this.montoEjecutado === 0) {
        return 0;
      } else {
        return ((this.montoEjecutado / this.montoPresup) * 100).toFixed(0);
      }
    },
    barColor() {
      if (this.valorPorc > 100) {
        return "error";
      } else {
        return "success";
      }
    }
  }
};
</script>

<style></style>
