<template>
  <v-toolbar class="hidden-sm-and-down">
    <v-spacer></v-spacer>
    <v-toolbar-items>
      <v-btn text v-for="item in menuItems" :key="item.title" :to="item.link">
        <v-icon left dark>{{ item.icon }}</v-icon>
        {{ item.title }}
      </v-btn>
    </v-toolbar-items>
  </v-toolbar>
</template>

<script>
export default {
  name: "menu-reporte",
  data() {
    return {
      menuItems: [
        {
          icon: "pie_chart",
          title: "Sum. Conceptos",
          link: "/reportes/repconcepto"
        },
        {
          icon: "pie_chart",
          title: "Cash Flow",
          link: "/reportes/repcashflow"
        },
        {
          icon: "pie_chart",
          title: "Hist. Conceptos",
          link: "/reportes/histconcepto"
        }
      ]
    };
  }
};
</script>

<style></style>
