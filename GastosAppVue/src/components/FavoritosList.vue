<template>
  <v-select
    v-model="value"
    v-bind:items="lista"
    item-value="DescripFrecuenteId"
    item-text="Nombre"
    label="Favorito"
    single-line
    bottom
  ></v-select>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "favoritos-list",
  props: ["selectedValue"],
  data() {
    return { value: 0 };
  },
  computed: {
    ...mapGetters({
      lista: "allFavoritosActivos"
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
