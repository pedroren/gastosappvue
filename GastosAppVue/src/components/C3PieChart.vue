<template>
  <div id="chart"></div>
</template>

<script>
// import d3 from 'd3'
import c3 from "c3";

export default {
  name: "c3-pie-chart",
  props: ["inputData"],
  data() {
    return {
      chartData: []
    };
  },
  watch: {
    inputData(newVal) {
      let data = this.inputData;
      let chartData = this.chartData;
      for (var i = 0; i < data.length; i++) {
        if (data[i].EsGasto) {
          let newItem = [data[i].ConceptoNombre, data[i].Value];
          newItem[1] *= -1;
          if (newItem[1] > 0) {
            chartData.push(newItem);
          }
        }
      }

      // var chart = c3.generate({
      c3.generate({
        bindto: "#chart",
        data: {
          columns: chartData,
          type: "pie"
        },
        tooltip: {
          format: {
            // title: function (d) { return 'Monto'; },
            value: function(value, ratio, id) {
              return value;
              // var format = d3.format(',')
              // return ' ' + format(value)
            }
          }
        }
      });
    }
  }
};
</script>

<style>
#chart {
  height: 400px;
}
</style>
