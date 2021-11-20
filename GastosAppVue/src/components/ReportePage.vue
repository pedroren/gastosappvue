<template>
  <v-container>
    <v-layout row>
      <menu-reporte></menu-reporte>
    </v-layout>
    <v-layout row>
      <v-list class="hidden-md-and-up">
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
          <v-list-item-content> </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-layout>
  </v-container>
</template>

<script>
import MenuReporte from "./MenuReporte";
export default {
  components: {
    MenuReporte
  },
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
