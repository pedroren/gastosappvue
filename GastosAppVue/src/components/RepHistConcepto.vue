<template>
  <v-container>
    <v-layout row>
      <menu-reporte></menu-reporte>
    </v-layout>
    <v-layout row wrap slot="extension">
      <trans-filter
        no-cuenta
        :filtroCuenta.sync="filtroCuenta"
        :filtroConcepto.sync="filtroConcepto"
        :filtroFechaDesde.sync="filtroFechaDesde"
        :filtroFechaHasta.sync="filtroFechaHasta"
      ></trans-filter>
      <v-btn @click="refresh">Refresh</v-btn>
      <v-progress-circular
        v-if="loading"
        indeterminate
        color="primary"
      ></v-progress-circular>
    </v-layout>
    <v-layout row wrap>
      <v-flex xs12 sm12>
        <v-card>
          <v-card-text height="500px">
            <am-line-chart :inputData="tableData"></am-line-chart>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import ReportesAPI from "../api/reportes";
import AMLineChart from "./AMLineChart";
import TransFilter from "./TransFilter.vue";
import MenuReporte from "./MenuReporte";

export default {
  components: {
    //GLineChart,
    "am-line-chart": AMLineChart,
    TransFilter,
    MenuReporte
  },
  data() {
    return {
      tableData: [],
      headers: [
        { text: "Concepto", value: "ConceptoId" },
        { text: "Monto", value: "Monto" }
      ],
      loading: true,
      filtroCuenta: null,
      filtroConcepto: null,
      filtroFechaDesde: this.$store.getters.firstDateString,
      filtroFechaHasta: this.$store.getters.todayDateString
    };
  },
  methods: {
    refresh() {
      this.loading = true;
      ReportesAPI.getHistConcepto(
        this.filtroFechaDesde,
        this.filtroFechaHasta,
        this.filtroConcepto
      ).then(response => {
        this.tableData = response.data;
        // this.paintChart(this.tableData)
        this.loading = false;
      });
    }
  },
  mounted() {
    this.refresh();
  }
};
</script>

<style></style>
