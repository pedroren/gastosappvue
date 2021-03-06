import Vue from "vue";
// import Vuetify from 'vuetify'
import Vuetify from "vuetify/lib";
// import 'vuetify/dist/vuetify.min.css'
import colors from "vuetify/lib/util/colors";

Vue.use(Vuetify, {});

export default new Vuetify({
  icons: {
    iconfont: "mdi"
  },
  theme: {
    themes: {
      light: {
        primary: colors.green
      },
      dark: {
        primary: colors.green.darken3
      }
    }
  }
});
