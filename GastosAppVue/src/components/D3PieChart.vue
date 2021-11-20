<template>
  <div id="chart">
    <svg></svg>
  </div>
</template>

<script>
import d3 from "d3";
import nv from "nvd3";

export default {
  name: "d3-pie-chart",
  props: ["data"],
  data() {
    return {
      value: 0
    };
  },
  watch: {
    data() {
      let data = this.data;
      let chartData = [];
      for (var i = 0; i < data.length; i++) {
        if (data[i].EsGasto) {
          let newItem = { label: data[i].ConceptoNombre, value: data[i].Value };
          newItem.value *= -1;
          if (newItem.value > 0) {
            chartData.push(newItem);
          }
        }
      }

      var chart = nv.models
        .pieChart()
        .x(function(d) {
          return d.label;
        })
        .y(function(d) {
          return d.value;
        })
        .showLabels(true)
        .labelType("percent"); //Configure what type of data to show in the label. Can be "key", "value" or "percent"

      d3.select("#chart svg")
        .datum(chartData)
        .transition()
        .duration(350)
        .call(chart);
    }
  }
};
</script>

<style>
#chart svg {
  height: 400px;
}
</style>
