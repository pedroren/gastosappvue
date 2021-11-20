<template>
  <v-container>
    <v-layout row>
      <menu-reporte></menu-reporte>
    </v-layout>
    <v-layout row wrap slot="extension">
      <trans-filter
        no-concepto
        no-cuenta
        :filtroCuenta.sync="filtroCuenta"
        :filtroConcepto.sync="filtroConcepto"
        :filtroFechaDesde.sync="filtroFechaDesde"
        :filtroFechaHasta.sync="filtroFechaHasta"
      ></trans-filter>
      <v-btn @click="refresh">Refresh</v-btn>
    </v-layout>
    <v-layout row wrap>
      <v-flex xs12 sm4>
        <v-data-table :headers="headers" :items="tableData" :loading="loading">
          <template slot="item" slot-scope="props">
            <tr>
              <td>{{ props.item.ConceptoNombre }}</td>
              <td class="text-xs-right">{{ props.item.Value | currency }}</td>
            </tr>
          </template>
        </v-data-table>
      </v-flex>
      <v-flex xs12 sm8>
        <v-card>
          <v-card-text height="500px">
            <!-- <g-pie-chart :inputData="tableData"></g-pie-chart> -->
            <am-pie-chart :inputData="tableData"></am-pie-chart>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import ReportesAPI from "../api/reportes";
//import GPieChart from './GPieChart'
import AMPieChart from "./AMPieChart";
import TransFilter from "./TransFilter.vue";
import MenuReporte from "./MenuReporte";

export default {
  components: {
    //GPieChart,
    "am-pie-chart": AMPieChart,
    TransFilter,
    MenuReporte
  },
  data() {
    return {
      tableData: [],
      headers: [
        { text: "Concepto", value: "ConceptoNombre" },
        { text: "Monto", value: "Value" }
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
      ReportesAPI.getSumCashFlow(
        this.filtroFechaDesde,
        this.filtroFechaHasta
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
