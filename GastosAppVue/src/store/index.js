import Vuex from "vuex";
import Vue from "vue";
import globals from "./globals";
import usuarios from "./usuarios";
import cuentas from "./cuentas";
import conceptos from "./conceptos";
import trans from "./trans";
import * as actions from "./actions";

Vue.use(Vuex);

export default new Vuex.Store({
  actions,
  modules: {
    globals,
    usuarios,
    cuentas,
    conceptos,
    trans
  }
});
