<template>
  <span>{{ NombreCuenta }}</span>
</template>

<script>
import store from "../store";

export default {
  props: ["TipoCuentaId"],
  data() {
    return {
      NombreCuenta: ""
    };
  },
  methods: {
    setNombre() {
      if (this.TipoCuentaId !== null) {
        let lista = store.getters.allTipoCuentas;
        // let i = lista.map(item => item.TipoCuentaId).indexOf(this.TipoCuentaId)
        let tp = lista.find(item => item.TipoCuentaId === this.TipoCuentaId);
        this.NombreCuenta = tp.Nombre;
      }
    }
  },
  mounted() {
    this.setNombre();
  },
  watch: {
    TipoCuentaId() {
      this.setNombre();
    }
  }
};
</script>

<style></style>
