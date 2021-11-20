import Vue from "vue";
import Vuex from "vuex";
import CuentasAPI from "../api/cuentas";

Vue.use(Vuex);

// state
const state = {
  lista: [],
  listaTipos: [],
  listaMonedas: [],
  cuentasLoaded: false
};

// getters
const getters = {
  allCuentas: state => state.lista,
  allCuentasActivas: state =>
    state.lista.filter(cuenta => {
      return cuenta.Activo;
    }),
  cuentasCount: state => state.lista.length,
  cuentasLoaded: state => state.cuentasLoaded,
  allTipoCuentas: state => state.listaTipos,
  allMonedas: state => state.listaMonedas
};

// mutations
const mutations = {
  set_cuentas(state, { cuentas }) {
    state.lista = cuentas;
    state.cuentasLoaded = true;
  },
  set_tipo_cuentas(state, { tipoCuentas }) {
    state.listaTipos = tipoCuentas;
  },
  set_monedas(state, { monedas }) {
    state.listaMonedas = monedas;
  }
};

// actions
const actions = {
  getAllCuentas({ commit }) {
    // var data = [{CuentaId: 0, Nombre: 'N/A'}, {CuentaId: 1, Nombre: 'Cuenta 1'}, {CuentaId: 2, Nombre: 'Cuenta 2'}]
    if (state.lista.length === 0) {
      commit("incLoading");
      CuentasAPI.getAll()
        .then(response => {
          commit("decLoading");
          commit("set_cuentas", { cuentas: response.data });
        })
        .catch(error => {
          // alert(error)
          commit("decLoading");
          error.message = "getCuentas: " + error.message;
          commit("setError", error);
        });
    }
  },
  refreshCuentas({ commit }) {
    commit("incLoading");
    CuentasAPI.getAll()
      .then(response => {
        commit("decLoading");
        commit("set_cuentas", { cuentas: response.data });
      })
      .catch(error => {
        commit("decLoading");
        error.message = "getCuentas: " + error.message;
        commit("setError", error);
      });
  },
  getTipoCuentas({ commit }) {
    if (state.listaTipos.length === 0) {
      commit("incLoading");
      CuentasAPI.getTipos()
        .then(response => {
          commit("decLoading");
          commit("set_tipo_cuentas", { tipoCuentas: response.data });
        })
        .catch(error => {
          commit("decLoading");
          error.message = "getTipoCuentas: " + error.message;
          commit("setError", error);
        });
    }
  },
  getMonedas({ commit }) {
    if (state.listaMonedas.length === 0) {
      commit("incLoading");
      CuentasAPI.getMonedas()
        .then(response => {
          commit("decLoading");
          commit("set_monedas", { monedas: response.data });
        })
        .catch(error => {
          commit("decLoading");
          error.message = "getMonedas: " + error.message;
          commit("setError", error);
        });
    }
  },
  refreshMonedas({ commit }) {
    commit("incLoading");
    CuentasAPI.getMonedas()
      .then(response => {
        commit("decLoading");
        commit("set_monedas", { monedas: response.data });
      })
      .catch(error => {
        commit("decLoading");
        error.message = "getMonedas: " + error.message;
        commit("setError", error);
      });
  }
};

export default {
  state,
  getters,
  mutations,
  actions
};
