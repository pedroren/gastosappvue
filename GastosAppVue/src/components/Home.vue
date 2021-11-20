<template>
  <v-container>
    <v-layout column>
      <v-flex>
        <v-card>
          <h1>{{ msg }}</h1>
          <v-btn @click="openLogin">login</v-btn>
          <login-dialog :dialog.sync="logindialog"></login-dialog>
          <v-btn color="pink" dark class="hidden-md-and-up" @click="newItem">
            <v-icon>add</v-icon>
          </v-btn>
          <v-btn
            color="pink"
            dark
            absolute
            bottom
            right
            fab
            class="hidden-sm-and-down"
            @click="newItem"
            ><v-icon>add</v-icon>
          </v-btn>
          <trans-create-edit
            :showDialog.sync="dialog"
            :transaccionId.sync="transaccionId"
            v-if="renderComponent"
          ></trans-create-edit>
          <v-progress-circular
            v-if="loading"
            indeterminate
            color="primary"
          ></v-progress-circular>
        </v-card>
      </v-flex>
      <v-flex>
        <v-card :loading="loading">
          <v-select
            v-model="presupId"
            v-bind:items="presupList"
            item-value="PresupuestoId"
            item-text="Nombre"
            label="Presupuesto"
            single-line
            bottom
          ></v-select>
          <div id="chartPresup"></div>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
// import am4themesAnimated from '@amcharts/amcharts4/themes/animated'
import LoginDialog from "./LoginDialog";
import TransCreateEdit from "./TransCreateEdit";
import PresupuestosAPI from "../api/presupuestos";
import ReportesAPI from "../api/reportes";

export default {
  name: "Home",
  components: {
    LoginDialog,
    TransCreateEdit
  },
  data() {
    return {
      msg: "Welcome to MoneyTrace",
      logindialog: false,
      loading: false,
      dialog: false,
      transaccionId: 0,
      presupList: [],
      presupId: null,
      presupData: [],
      chartPresup: null,
      renderComponent: false
    };
  },
  computed: mapGetters(["allCuentasActivas", "loggedUser"]),
  methods: {
    ...mapActions(["setMessage", "setError"]),
    openLogin() {
      // this.LoginDialog.open()
      this.logindialog = true;
    },
    newItem() {
      this.transaccionId = 0;
      this.dialog = true;
    },
    loadPresupList() {
      this.presupId = null;
      PresupuestosAPI.getAllDesc()
        .then(response => {
          this.presupList = response.data;
          if (this.presupList.length > 0) {
            this.presupId = this.presupList[0].PresupuestoId;
          }
        })
        .catch(error => {
          this.setError(error);
        });
    },
    showChartPresup() {
      this.loading = true;
      ReportesAPI.getRepPresupuesto(this.presupId).then(response => {
        this.presupData = response.data;
        // this.paintChart(this.tableData)
        this.loading = false;
      });
    },
    drawchartPresup(dataParam) {
      this.loading = true;

      if (this.chartPresup) {
        this.chartPresup.dispose();
      }
      // Create chart instance
      var chart = (this.chartPresup = am4core.create(
        "chartPresup",
        am4charts.XYChart
      ));
      // Add percent sign to all numbers
      //chart.numberFormatter.numberFormat = "#.3'%'";

      chart.data = dataParam;

      // Create axes
      var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
      categoryAxis.dataFields.category = "ConceptoNombre";
      categoryAxis.renderer.grid.template.location = 0;
      categoryAxis.renderer.minGridDistance = 30;

      var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
      valueAxis.title.text = "Presupuestado";
      valueAxis.title.fontWeight = 800;

      // Create series
      var series = chart.series.push(new am4charts.ColumnSeries());
      series.dataFields.valueY = "MontoPresupuesto";
      series.dataFields.categoryX = "ConceptoNombre";
      series.clustered = false;
      series.tooltipText = "Presupuestado {categoryX}: [bold]{valueY}[/]";

      var series2 = chart.series.push(new am4charts.ColumnSeries());
      series2.dataFields.valueY = "MontoEjecutado";
      series2.dataFields.categoryX = "ConceptoNombre";
      series2.clustered = false;
      series2.columns.template.width = am4core.percent(60);
      series2.tooltipText = "Ejecutado {categoryX}: [bold]{valueY}[/]";

      chart.cursor = new am4charts.XYCursor();
      chart.cursor.lineX.disabled = true;
      chart.cursor.lineY.disabled = true;

      this.loading = false;
    },
    forceRerender() {
      console.log("forceRerender");
      // Remove my-component from the DOM
      this.renderComponent = false;

      this.$nextTick(() => {
        // Add the component back in
        this.renderComponent = true;
      });
    }
  },
  mounted() {
    if (this.loggedUser) {
      this.loadPresupList();
      if (this.loggedUser === "invitado") {
        this.$nextTick(() => {
          this.openLogin();
        });
      }
    }
  },
  watch: {
    loggedUser() {
      console.log("watched loggedUser");
      if (this.loggedUser) {
        this.loadPresupList();
        this.forceRerender();
      }
    },
    presupId() {
      if (this.loggedUser && this.presupId) this.showChartPresup();
    },
    presupData() {
      if (this.presupData.length > 0) {
        this.drawchartPresup(this.presupData);
      }
    }
  }
};
</script>
