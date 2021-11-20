<template>
  <v-container>
    <v-layout row wrap>
      <v-flex xs6 sm3>
        <date-picker-textbox
          label="Fecha Hasta"
          :fecha.sync="filtroFechaDesdeValue"
        ></date-picker-textbox>
      </v-flex>
      <v-flex xs6 sm3>
        <date-picker-textbox
          label="Fecha Hasta"
          :fecha.sync="filtroFechaHastaValue"
        ></date-picker-textbox>
      </v-flex>
      <v-flex xs6 sm3 v-if="!NoCuenta">
        <cuentas-list :selectedValue.sync="filtroCuentaValue"></cuentas-list>
      </v-flex>
      <v-flex xs6 sm3 v-if="!NoConcepto">
        <conceptos-list
          :selectedValue.sync="filtroConceptoValue"
        ></conceptos-list>
      </v-flex>
    </v-layout>
  </v-container>
</template>
<script>
import ConceptosList from "./ConceptosList.vue";
import CuentasList from "./CuentasList.vue";
import { mapGetters } from "vuex";

export default {
  name: "TransFilter",
  components: {
    CuentasList,
    ConceptosList
  },
  props: {
    filtroFechaDesde: String,
    filtroFechaHasta: String,
    filtroConcepto: Number,
    filtroCuenta: Number,
    NoCuenta: {
      type: Boolean,
      default: false
    },
    NoConcepto: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      filtroFechaDesdeValue: null,
      filtroFechaHastaValue: null,
      filtroConceptoValue: 0,
      filtroCuentaValue: 0,
      menu1: false,
      menu2: false
    };
  },
  methods: {
    onChangeCuenta(newValue) {
      console.log("update newValue" + newValue);
      // this.filtroCuenta = newValue
    },
    onChangeConcepto() {
      this.filtroConcepto = ConceptosList.data.selectedValue;
    }
  },
  computed: mapGetters(["todayDateString", "firstDateString"]),
  watch: {
    filtroFechaDesdeValue: function(newVal) {
      this.$emit("update:filtroFechaDesde", newVal);
    },
    filtroFechaHastaValue: function(newVal) {
      this.$emit("update:filtroFechaHasta", newVal);
    },
    filtroConceptoValue: function(newVal) {
      this.$emit("update:filtroConcepto", newVal);
    },
    filtroCuentaValue: function(newVal) {
      this.$emit("update:filtroCuenta", newVal);
    }
  },
  mounted() {
    if (this.todayDateString) {
      this.filtroFechaDesdeValue = this.firstDateString;
      this.filtroFechaHastaValue = this.todayDateString;
    }
  }
};
</script>
