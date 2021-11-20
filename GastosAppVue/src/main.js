// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
// import '@babel/polyfill'
import Vue from "vue";
import vuetify from "./plugins/vuetify";
import App from "./App.vue";
import router from "./router";
import store from "./store/index";
import DateFilter from "./filters/date";
import CurrencyFilter from "./filters/currency";
import AlertComp from "./components/Alert";
import TipoCuentaNombre from "./components/TipoCuentaNombre";
import MonedaNombre from "./components/MonedaNombre";
import ConceptosList from "./components/ConceptosList";
import ConceptoNombre from "./components/ConceptoNombre";
import CuentasList from "./components/CuentasList";
import CuentaNombre from "./components/CuentaNombre";
import DatePickerTextbox from "./components/DatePickerTextbox";

Vue.config.productionTip = false;
global.store = store;

Vue.filter("date", DateFilter);
Vue.filter("currency", CurrencyFilter);
Vue.component("app-alert", AlertComp);
Vue.component("tipo-cuenta-nombre", TipoCuentaNombre);
Vue.component("moneda-nombre", MonedaNombre);
Vue.component("conceptos-list", ConceptosList);
Vue.component("concepto-nombre", ConceptoNombre);
Vue.component("cuentas-list", CuentasList);
Vue.component("cuenta-nombre", CuentaNombre);
Vue.component("date-picker-textbox", DatePickerTextbox);

/* eslint-disable no-new */
new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount("#app");
