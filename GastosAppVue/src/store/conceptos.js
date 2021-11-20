import Vue from "vue";
import Vuex from "vuex";
import ConceptosAPI from "../api/conceptos";

Vue.use(Vuex);

// state
const state = {
  lista: [],
  conceptosLoaded: false
};

// getters
const getters = {
  allConceptos: state => state.lista,
  allConceptosActivas: state =>
    state.lista.filter(concepto => {
      return concepto.Activo;
    }),
  conceptosCount: state => state.lista.length,
  conceptosLoaded: state => state.conceptosLoaded
};

// mutations
const mutations = {
  set_conceptos(state, { conceptos }) {
    state.lista = conceptos;
    state.conceptosLoaded = true;
  }
};

// actions
const actions = {
  getAllConceptos({ commit }) {
    if (state.lista.length === 0) {
      commit("incLoading");
      ConceptosAPI.getAll()
        .then(response => {
          commit("decLoading");
          commit("set_conceptos", { conceptos: response.data });
        })
        .catch(error => {
          // alert(error)
          commit("decLoading");
          error.message = "getConceptos: " + error.message;
          commit("setError", error);
        });
    }
  },
  refreshConceptos({ commit }) {
    commit("incLoading");
    ConceptosAPI.getAll()
      .then(response => {
        commit("decLoading");
        commit("set_conceptos", { conceptos: response.data });
      })
      .catch(error => {
        // alert(error)
        commit("decLoading");
        error.message = "getConceptos: " + error.message;
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
