<template>
  <v-container>
    <v-layout row>
      <v-flex xs12>
        <form @submit.prevent="doLogin">
          <v-layout row>
            <v-flex>
              <v-text-field
                name="UserName"
                id="UserName"
                label="UserName"
                v-model="userName"
                required
              ></v-text-field>
            </v-flex>
          </v-layout>
          <v-layout row>
            <v-flex>
              <v-text-field
                name="Password"
                id="Password"
                label="Password"
                v-model="password"
                required
                type="password"
              ></v-text-field>
            </v-flex>
          </v-layout>
          <v-layout row>
            <v-flex>
              <v-btn class="primary" :disabled="!formIsValid" type="submit"
                >Login</v-btn
              >
              <v-btn @click="cancel">Cancel</v-btn>
            </v-flex>
          </v-layout>
        </form>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
export default {
  name: "SignIn",
  data() {
    return {
      userName: "",
      password: ""
    };
  },
  computed: {
    formIsValid() {
      return this.userName !== "" && this.password !== "";
    }
  },
  methods: {
    doLogin() {
      this.$store.dispatch("setUsuario", { usuario: this.userName });
      this.$store.dispatch("refreshMonedas");
      this.$store.dispatch("refreshConceptos");
      this.$store.dispatch("refreshCuentas");
      this.$store.dispatch("refreshFavoritos");
      console.log("loading", this.$store.getters.loading);
      // go back by one record, the same as history.back()
      // this.$router.go(-1)
      this.$emit("ok");
    },
    cancel() {
      this.$emit("cancel");
    }
  }
};
</script>

<style></style>
