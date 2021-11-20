<template>
  <v-flex>
    <v-progress-circular
      v-if="loading"
      indeterminate
      color="primary"
    ></v-progress-circular>
    <div ref="amchart_pie" style="height: 60vh;"></div>
  </v-flex>
</template>

<script>
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themesAnimated from "@amcharts/amcharts4/themes/animated";
//import am4themesMaterial from '@amcharts/amcharts4/themes/material'
import * as am4plugins_regression from "@amcharts/amcharts4/plugins/sliceGrouper";
// import { truncate } from 'fs';

am4core.useTheme(am4themesAnimated);
//am4core.useTheme(am4themesMaterial)

export default {
  name: "am-pie-chart",
  props: ["inputData"],
  data() {
    return {
      chartam: null,
      chartData: [],
      chartoptions: {
        width: "100%",
        is3D: true
      },
      loading: true
    };
  },
  methods: {
    drawchart(dataParam) {
      this.loading = true;
      if (this.chartam) {
        this.chartam.dispose();
      }
      let chart = am4core.create(this.$refs.amchart_pie, am4charts.PieChart3D);
      chart.legend = new am4charts.Legend();
      chart.exporting.menu = new am4core.ExportMenu();
      chart.responsive.enabled = true;
      let data = [];
      // console.log(dataParam)
      dataParam.forEach(function(row) {
        //console.log(row)
        if (row.Value < 0) {
          let newRow = {
            Concepto: row.ConceptoNombre,
            Monto: row.Value * -1
          };
          data.push(newRow);
        }
      });
      //console.log(data)
      chart.data = data;
      //var colorSet = new am4core.ColorSet()
      //colorSet.list = this.colores.map(function (color) {
      //  return am4core.color(color)
      //})
      let serie = chart.series.push(new am4charts.PieSeries3D());
      serie.dataFields.value = "Monto";
      serie.innerRadius = am4core.percent(10);
      serie.dataFields.category = "Concepto";
      serie.labels.template.disabled = true;
      serie.ticks.template.disabled = true;
      //serie.colors = colorSet

      let grouper = serie.plugins.push(
        new am4plugins_regression.SliceGrouper()
      );
      grouper.threshold = 5;
      grouper.groupName = "Otros";
      grouper.clickBehavior = "break";

      this.chartam = chart;
      this.loading = false;
    },
    dochart() {}
  },
  watch: {
    inputData() {
      this.drawchart(this.inputData);
    }
  }
};
</script>

<style></style>
