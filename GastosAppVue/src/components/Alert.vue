<template>
  <v-layout>
    <v-snackbar v-model="errorEnabled" color="error" :timeout="errorTimeout">
      {{ errorMessage }}
      <v-btn text dark @click.native="closeError">Close</v-btn>
    </v-snackbar>
    <v-snackbar v-model="messageEnabled" color="success">
      {{ message }}
      <v-btn text dark @click.native="closeMessage">Close</v-btn>
    </v-snackbar>
  </v-layout>
</template>

<script>
export default {
  data() {
    return {
      errorEnabled: false,
      messageEnabled: false,
      errorTimeout: 90000
    };
  },
  methods: {
    onClose() {
      this.$emit("dismissed");
    },
    closeMessage() {
      this.$store.dispatch("clearMessage");
    },
    closeError() {
      this.$store.dispatch("clearError");
    }
  },
  computed: {
    error() {
      return this.$store.getters.error;
    },
    errorMessage() {
      if (this.$store.getters.error != null) {
        return this.$store.getters.error.message;
      } else {
        return null;
      }
    },
    message() {
      return this.$store.getters.message;
    },
    showError() {
      return this.$store.getters.error != null;
    },
    showMessage() {
      return this.$store.getters.message != null;
    }
  },
  watch: {
    showError(newValue) {
      this.errorEnabled = newValue;
    },
    showMessage(newValue) {
      this.messageEnabled = newValue;
    },
    messageEnabled() {
      if (
        this.messageEnabled === false &&
        this.$store.getters.message != null
      ) {
        this.closeMessage();
      }
    }
  }
};
</script>

<style></style>
