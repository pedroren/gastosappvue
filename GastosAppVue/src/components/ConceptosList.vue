<template>
  <v-autocomplete
    v-model="value"
    v-bind:items="conceptos"
    item-value="ConceptoId"
    item-text="NombreConPadre"
    label="Concepto"
    single-line
    clearable
  ></v-autocomplete>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "conceptos-list",
  props: ["selectedValue"],
  data() {
    return { value: 0 };
  },
  created() {
    // this.$store.dispatch('getAllConceptos')
  },
  mounted() {
    if (this.selectedValue !== undefined) {
      this.value = this.selectedValue;
    }
  },
  computed: {
    ...mapGetters({
      conceptos: "allConceptosActivas",
      length: "conceptosCount"
    })
  },
  watch: {
    selectedValue: function() {
      if (this.selectedValue !== undefined) {
        this.value = this.selectedValue;
      }
    },
    value: function(newVal) {
      this.$emit("update:selectedValue", newVal);
    }
  }
};
</script>
