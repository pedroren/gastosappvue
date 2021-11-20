<template>
  <!--<div id="app">
    <img src="./assets/logo.png">
    <router-view/>
  </div>-->
  <v-app>
    <v-navigation-drawer
      temporary
      :mini-variant="miniVariant"
      :clipped="clipped"
      v-model="drawer"
      enable-resize-watcher
      fixed
      app
    >
      <v-list>
        <v-list-item
          value="true"
          v-for="(item, i) in menuItems"
          :key="i"
          :to="item.link"
        >
          <v-list-item-action>
            <v-icon v-html="item.icon"></v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title v-text="item.title"></v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-list-item>
          <v-list-item-content>
            <slot name="navdrawer"></slot>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-app-bar app :color="barcolor" :dark="true" :clipped-left="clipped">
      <v-app-bar-nav-icon
        class="hidden-md-and-up"
        @click.stop="drawer = !drawer"
      ></v-app-bar-nav-icon>
      <v-toolbar-title
        ><router-link to="/" tag="span" style="cursor: pointer">
          MoneyTrace
          <v-progress-circular
            v-if="loading"
            indeterminate
            color="primary"
          ></v-progress-circular> </router-link
      ></v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items class="hidden-sm-and-down">
        <v-btn text v-for="item in menuItems" :key="item.title" :to="item.link">
          <v-icon left dark>{{ item.icon }}</v-icon>
          {{ item.title }}
        </v-btn>
        <v-btn text key="usuario" to="/login">
          <v-icon left dark>account_box</v-icon>
          {{ currentUser }}
        </v-btn>
      </v-toolbar-items>
    </v-app-bar>
    <v-content>
      <v-container fluid align-content-start>
        <v-layout row>
          <router-view></router-view>
        </v-layout>
      </v-container>
    </v-content>
    <v-footer app></v-footer>
    <v-layout row>
      <v-flex xs12 sm6 offset-sm3>
        <app-alert @dismissed="onDismissed"></app-alert>
      </v-flex>
    </v-layout>
  </v-app>
</template>

<script>
import store from "./store";

export default {
  name: "App",
  store,
  data() {
    return {
      barcolor: "light-green darken-4",
      clipped: false,
      drawer: false,
      miniVariant: false,
      menuItems: [
        {
          icon: "date_range",
          title: "Transacciones",
          link: "/transacciones"
        },
        {
          icon: "speaker_notes",
          title: "Facturas",
          link: "/facturas"
        },
        {
          icon: "shop",
          title: "Cuentas",
          link: "/cuentas"
        },
        {
          icon: "receipt",
          title: "Mantenimientos",
          link: "/mantenimientos"
        },
        {
          icon: "pie_chart",
          title: "Reportes",
          link: "/reportes"
        }
      ],
      userIsAuthenticated: true
    };
  },
  created() {
    console.log("loading", this.currentUser);
    this.$store.dispatch("setUsuario", { usuario: "invitado" });
    this.$store.dispatch("initializeDates");
    this.$store.dispatch("getTiposTrans");
    this.$store.dispatch("getMonedas");
    this.$store.dispatch("getTipoCuentas");
    this.$store.dispatch("getAllConceptos");
    this.$store.dispatch("getAllCuentas");
    this.$store.dispatch("getNewTransTemplate");
    this.$store.dispatch("getFavoritos");
  },
  computed: {
    loading() {
      return this.$store.getters.loading > 0;
    },
    error() {
      return this.$store.getters.error;
    },
    currentUser() {
      return this.$store.getters.loggedUser;
    }
  },
  methods: {
    onDismissed() {
      this.$store.dispatch("clearError");
    }
  }
};
</script>
