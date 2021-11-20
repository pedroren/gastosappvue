<template>
  <v-container>
    <v-layout row>
      <menu-reporte></menu-reporte>
    </v-layout>
    <v-layout row wrap slot="extension">
      <trans-filter
        no-concepto
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
            <tr @click="detItem(props.item)">
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
    <trans-list
      :showDialog.sync="dialogDetalle"
      :filtroFechaDesde="filtroFechaDesde"
      :filtroFechaHasta="filtroFechaHasta"
      :filtroConcepto="selConceptoId"
    ></trans-list>
  </v-container>
</template>

<script>
import ReportesAPI from "../api/reportes";
//import GPieChart from './GPieChart'
import AMPieChart from "./AMPieChart";
import TransFilter from "./TransFilter.vue";
import MenuReporte from "./MenuReporte";
import TransList from "./TransList";

export default {
  components: {
    //GPieChart,
    "am-pie-chart": AMPieChart,
    TransFilter,
    MenuReporte,
    TransList
  },
  data() {
    return {
      tableData: [],
      headers: [
        { text: "Concepto", value: "ConceptoId" },
        { text: "Monto", value: "Value" }
      ],
      loading: true,
      filtroCuenta: null,
      filtroConcepto: null,
      filtroFechaDesde: this.$store.getters.firstDateString,
      filtroFechaHasta: this.$store.getters.todayDateString,
      dialogDetalle: false,
      selConceptoId: null
    };
  },
  methods: {
    refresh() {
      this.loading = true;
      ReportesAPI.getSumConcepto(
        this.filtroFechaDesde,
        this.filtroFechaHasta,
        this.filtroCuenta
      ).then(response => {
        this.tableData = response.data;
        // this.paintChart(this.tableData)
        this.loading = false;
      });
    },
    detItem(item) {
      // this.facturaId = item.FacturaId
      this.selConceptoId = item.ConceptoId;
      this.dialogDetalle = true;
    }
  },
  mounted() {
    this.refresh();
  }
};
</script>

<style></style>
