<template>
  <span>{{ NombreCuenta }}</span>
</template>

<script>
import store from "../store";

export default {
  props: ["CuentaId"],
  data() {
    return {
      NombreCuenta: ""
    };
  },
  methods: {
    setNombre() {
      if (this.CuentaId !== null) {
        let lista = store.getters.allCuentas;
        let cuenta = lista.find(item => item.CuentaId === this.CuentaId);
        this.NombreCuenta = cuenta.Nombre;
      }
    }
  },
  mounted() {
    this.setNombre();
  },
  watch: {
    CuentaId() {
      this.setNombre();
    }
  }
};
</script>

<style></style>
