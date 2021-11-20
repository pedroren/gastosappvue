<template>
  <v-flex>
    <v-progress-circular
      v-if="loading"
      indeterminate
      color="primary"
    ></v-progress-circular>
    <div ref="amchart_xy" style="height: 60vh;"></div>
  </v-flex>
</template>

<script>
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themesAnimated from "@amcharts/amcharts4/themes/animated";
// import am4themesMaterial from '@amcharts/amcharts4/themes/material'
// import * as am4plugins_regression from "@amcharts/amcharts4/plugins/sliceGrouper";

am4core.useTheme(am4themesAnimated);
// am4core.useTheme(am4themesMaterial)

export default {
  name: "am-line-chart",
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
      let chart = am4core.create(this.$refs.amchart_xy, am4charts.XYChart);
      chart.legend = new am4charts.Legend();
      chart.exporting.menu = new am4core.ExportMenu();
      chart.responsive.enabled = true;
      var xAxis = chart.xAxes.push(new am4charts.CategoryAxis());
      xAxis.dataFields.category = "AnoMes";
      var yAxis = chart.yAxes.push(new am4charts.ValueAxis());
      yAxis.title.text = "Monto";
      yAxis.renderer.inversed = true;
      let data = [];
      chart.data = data;
      this.chartam = chart;

      // build list of date
      var dateValues = [];
      dataParam.forEach(function(row) {
        dateValues.push(row.AnoMes);
        let line = { AnoMes: row.AnoMes };
        row.Conceptos.forEach(function(c) {
          line[c.ConceptoNombre] = c.TotalMonto;
        });
        data.push(line);
      });

      console.log("dateValues", dateValues);

      // build list of series
      var chartCols = [];
      dataParam.forEach(function(row) {
        row.Conceptos.forEach(function(dateValue) {
          if (chartCols.indexOf(dateValue.ConceptoNombre) === -1) {
            chartCols.push(dateValue.ConceptoNombre);
          }
        });
      });

      console.log("chartCols", chartCols);
      //var colorSet = new am4core.ColorSet()
      //colorSet.list = this.colores.map(function (color) {
      //  return am4core.color(color)
      //})
      chartCols.forEach(function(concepto) {
        let serie = chart.series.push(new am4charts.LineSeries());
        serie.hidden = true;
        //console.log('concepto', concepto)
        serie.title = "ConceptoNombre";
        serie.name = concepto;
        serie.dataFields.valueY = concepto;
        serie.dataFields.categoryX = "AnoMes";
        var bullet = serie.bullets.push(new am4charts.Bullet());
        var rectangle = bullet.createChild(am4core.Rectangle);
        rectangle.width = 10;
        rectangle.height = 10;
        bullet.stroke = am4core.color("#000");
        bullet.fill = am4core.color("#000");
        bullet.tooltipText = "{name}: [bold]{valueY}[/]"; // Todos los tooltips juntos
        //serie.colors = colorSet
      });

      this.loading = false;
      //console.log('data', data)
      //chart.data = data
      //this.chartam = chart
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
