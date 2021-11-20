<template>
  <GChart
    :settings="{ packages: ['corechart', 'table'] }"
    type="LineChart"
    :data="chartData"
    :options="chartOptions"
    id="chart1"
  />
</template>

<script>
import { GChart } from "vue-google-charts";

export default {
  components: {
    GChart
  },
  props: ["inputData"],
  data() {
    return {
      chartData: [],
      chartReady: false,
      chartOptions: {
        vAxis: {
          direction: "-1"
        }
      }
    };
  },
  mounted() {
    // Load the Visualization API and the piechart package.
    /* GoogleCharts.load(onChartLoad)
    function onChartLoad () {
      // this.chartReady = true
    } */
  },
  watch: {
    inputData() {
      console.log("chart inputData");
      let data = this.inputData;
      this.chartData = [];
      let chartData = [];

      // build chart columns
      var chartCols = ["Ano-Mes"];
      data.forEach(function(row) {
        chartCols.push(row.ConceptoNombre);
      });

      console.log(chartCols);

      // build list of date
      var dateValues = [];
      data.forEach(function(row) {
        row.AnoMes.forEach(function(dateValue) {
          if (dateValues.indexOf(dateValue.AnoMes) === -1) {
            dateValues.push(dateValue.AnoMes);
          }
        });
      });

      console.log(dateValues);

      chartData = [chartCols];
      dateValues.forEach(function(dateValue) {
        var row = [dateValue];
        data.forEach(function(concepto) {
          let notfound = true;
          concepto.AnoMes.forEach(function(am) {
            if (am.AnoMes === dateValue) {
              row.push(am.TotalMonto);
              notfound = false;
            }
          });
          if (notfound) {
            row.push(null);
          }
        });
        chartData.push(row);
      });
      console.log(chartData);
      this.chartData = chartData;
      if (data.length > 0) {
        console.log("chart draw");
        // chart
        /* var options = {
          vAxis: {
            direction: '-1'
          }
        }
        // Standard google charts functionality is available as GoogleCharts.api after load
        const dataTable = GoogleCharts.api.visualization.arrayToDataTable(chartData)
        const pieChart = new GoogleCharts.api.visualization.LineChart(document.getElementById('chart1'))
        pieChart.draw(dataTable, options) */
      }
    }
  }
};
</script>

<style>
#chart1 {
  height: 400px;
}
</style>
