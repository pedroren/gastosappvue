<template>
  <span>{{ NombreMoneda }}</span>
</template>

<script>
import store from "../store";

export default {
  props: ["MonedaId"],
  data() {
    return {
      NombreMoneda: ""
    };
  },
  methods: {
    setNombre() {
      if (this.MonedaId !== null) {
        let lista = store.getters.allMonedas;
        let moneda = lista.find(item => item.MonedaId === this.MonedaId);
        this.NombreMoneda = moneda.Nombre;
      }
    }
  },
  mounted() {
    this.setNombre();
  },
  watch: {
    MonedaId() {
      this.setNombre();
    }
  }
};
</script>

<style></style>
