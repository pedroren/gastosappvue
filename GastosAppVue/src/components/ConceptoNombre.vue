<template>
  <span>{{ NombreConcepto }}</span>
</template>

<script>
import store from "../store";

export default {
  props: ["ConceptoId"],
  data() {
    return {
      NombreConcepto: ""
    };
  },
  methods: {
    setNombre() {
      if (this.ConceptoId !== null) {
        let lista = store.getters.allConceptos;
        let concepto = lista.find(item => item.ConceptoId === this.ConceptoId);
        this.NombreConcepto = concepto.Nombre;
      }
    }
  },
  mounted() {
    this.setNombre();
  },
  watch: {
    ConceptoId() {
      this.setNombre();
    }
  }
};
</script>

<style></style>
