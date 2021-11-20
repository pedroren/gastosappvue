<template>
  <v-menu
    ref="menu"
    :close-on-content-click="false"
    v-model="menu"
    transition="scale-transition"
    offset-y
    full-width
    :nudge-right="40"
    max-width="290px"
    min-width="200px"
    :return-value.sync="value"
  >
    <template v-slot:activator="{ on }">
      <v-text-field
        :label="getLabel"
        v-model="fecha"
        prepend-icon="event"
        readonly
        v-on="on"
      ></v-text-field>
    </template>
    <v-date-picker
      v-model="value"
      @input="$refs.menu.save(value)"
      no-title
      scrollable
    ></v-date-picker>
  </v-menu>
</template>

<script>
export default {
  props: ["fecha", "label"],
  data() {
    return {
      value: null,
      menu: false
    };
  },
  computed: {
    getLabel() {
      return this.label ? this.label : "Fecha";
    }
  },
  watch: {
    fecha: function() {
      if (this.fecha !== undefined) {
        this.value = this.fecha;
      }
    },
    value: function(newVal) {
      this.$emit("update:fecha", newVal);
    }
  }
};
</script>

<style></style>
