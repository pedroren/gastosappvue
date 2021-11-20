<template>
  <v-toolbar>
    <v-spacer></v-spacer>
    <v-toolbar-items class="hidden-sm-and-down">
      <v-btn text v-for="item in menuItems" :key="item.title" :to="item.link">
        <v-icon left dark>{{ item.icon }}</v-icon>
        {{ item.title }}
      </v-btn>
    </v-toolbar-items>
  </v-toolbar>
</template>

<script>
export default {
  name: "MenuMantenimiento",
  data() {
    return {
      menuItems: [
        {
          icon: "speaker_notes",
          title: "Conceptos",
          link: "/mantenimientos/conceptos"
        },
        {
          icon: "favorite",
          title: "Favoritos",
          link: "/mantenimientos/favoritos"
        },
        {
          icon: "monetization_on",
          title: "Monedas",
          link: "/mantenimientos/monedas"
        },
        {
          icon: "shopping_cart",
          title: "Presupuestos",
          link: "/mantenimientos/presupuestos"
        },
        {
          icon: "open_in_browser",
          title: "Importaciones",
          link: "/mantenimientos/importaciones"
        },
        {
          icon: "settings_input_component",
          title: "Parámetros",
          link: "/mantenimientos/parametros"
        }
      ]
    };
  }
};
</script>

<style></style>
