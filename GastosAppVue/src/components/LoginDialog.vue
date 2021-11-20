<template>
  <v-dialog v-model="dialog" max-width="300px">
    <v-card>
      <v-card-title>
        Sign In
      </v-card-title>
      <v-card-text>
        <sign-in v-on:ok="close" v-on:cancel="close"></sign-in>
      </v-card-text>
      <v-card-actions> </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import SignIn from "./SignIn";

export default {
  name: "LoginDialog",
  props: ["dialog"],
  components: {
    SignIn
  },
  data() {
    return {
      ok: false
    };
  },
  methods: {
    open() {
      // this.dialog = true
    },
    close() {
      // this.dialog = false
      this.$emit("update:dialog", false);
    }
  },
  watch: {
    value: function(newVal) {
      this.$emit("update:dialog", newVal);
    }
  }
};
</script>

<style></style>
