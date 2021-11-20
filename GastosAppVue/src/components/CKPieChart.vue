<template>
  <pie-chart
    :data="chartData"
    :legend="false"
    height="400px"
    prefix="$"
    thousands=","
  ></pie-chart>
</template>

<script>
import Vue from "vue";
import Chartkick from "chartkick";
import VueChartkick from "vue-chartkick";
// eslint-disable-next-line
import Chart from 'chart.js'

Vue.use(VueChartkick, { Chartkick });

export default {
  name: "ck-pie-chart",
  components: {
    Chart
  },
  props: ["inputData"],
  data() {
    return {
      chartData: []
    };
  },
  methods: {
    load() {}
  },
  created() {},
  watch: {
    inputData() {
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
    }
  }
};
</script>

<style></style>
