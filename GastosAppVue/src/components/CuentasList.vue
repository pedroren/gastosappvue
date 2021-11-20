<template>
  <v-autocomplete
    v-model="value"
    v-bind:items="cuentas"
    item-value="CuentaId"
    item-text="Nombre"
    :label="getLabel"
    single-line
    clearable
  ></v-autocomplete>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "cuentas-list",
  props: ["selectedValue", "label"],
  data() {
    return { value: 0 };
  },
  created() {
    // this.$store.dispatch('getAllCuentas')
    console.log("cuenta-list created");
  },
  mounted() {
    console.log("cuenta-list mounted");
    if (this.selectedValue !== undefined) {
      this.value = this.selectedValue;
    }
  },
  computed: {
    ...mapGetters({
      cuentas: "allCuentasActivas",
      length: "cuentasCount"
    }),
    getLabel() {
      return this.label ? this.label : "Cuenta";
    }
  },
  watch: {
    selectedValue: function(newVal) {
      if (newVal !== undefined && newVal !== this.value) {
        this.value = newVal;
      }
      console.log("cuenta-list selectedValue changed: " + newVal);
    },
    value: function(newVal) {
      if (newVal !== this.selectedValue) {
        this.$emit("update:selectedValue", newVal);
      }
      console.log("cuenta-list value changed: " + newVal);
    }
  }
};
</script>
