<template>
  <GChart
    type="PieChart"
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
        is3D: true,
        sliceVisibilityThreshold: 0.05
      }
    };
  },
  mounted() {
    // Load the Visualization API and the piechart package.
    /* GoogleCharts.load(onChartLoad)
    function onChartLoad () {
    } */
  },
  watch: {
    inputData() {
      // console.log('chart inputData')
      let data = this.inputData;
      this.chartData = [];
      let chartData = this.chartData;
      chartData.push(["Concepto", "Monto"]);
      for (var i = 0; i < data.length; i++) {
        if (data[i].Value < 0) {
          let newItem = [data[i].ConceptoNombre, data[i].Value];
          newItem[1] *= -1;
          chartData.push(newItem);
        }
      }

      if (data.length > 0) {
        // console.log('chart draw')
        // chart
        /* var options = {
          is3D: true,
          sliceVisibilityThreshold: 0.05
        } */
        // Standard google charts functionality is available as GoogleCharts.api after load
        /* const dataTable = GoogleCharts.api.visualization.arrayToDataTable(chartData)
         const pieChart = new GoogleCharts.api.visualization.PieChart(document.getElementById('chart1'))
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
