import Vue from "vue";
import Vuex from "vuex";
import TransaccionesAPI from "../api/transacciones";
import FavoritosAPI from "../api/favoritos";

Vue.use(Vuex);

// state
const state = {
  listaTipos: [],
  newTemplate: null,
  listaFavoritos: []
};

// getters
const getters = {
  allTiposTrans: state => state.listaTipos,
  newTransTemplate: state => state.newTemplate,
  allFavoritos: state => state.listaFavoritos,
  allFavoritosActivos: state =>
    state.listaFavoritos.filter(favorito => {
      return favorito.Activo;
    })
};

// mutations
const mutations = {
  set_lista_tipos(state, { listaTipos }) {
    state.listaTipos = listaTipos;
  },
  set_new_trans_template(state, { newTemplate }) {
    state.newTemplate = newTemplate;
  },
  set_lista_favoritos(state, { lista }) {
    state.listaFavoritos = lista;
  }
};
// actions
const actions = {
  getTiposTrans({ commit }) {
    if (state.listaTipos.length === 0) {
      commit("incLoading");
      TransaccionesAPI.getTiposTrans()
        .then(response => {
          commit("decLoading");
          commit("set_lista_tipos", { listaTipos: response.data });
        })
        .catch(error => {
          commit("decLoading");
          error.message = "getTiposTrans: " + error.message;
          commit("setError", error);
        });
    }
  },
  getNewTransTemplate({ commit }) {
    if (state.listaTipos.length === 0) {
      commit("incLoading");
      TransaccionesAPI.getNewRecord()
        .then(response => {
          commit("decLoading");
          commit("set_new_trans_template", { newTemplate: response.data });
        })
        .catch(error => {
          commit("decLoading");
          error.message = "getNewRecord: " + error.message;
          commit("setError", error);
        });
    }
  },
  getFavoritos({ commit }) {
    if (state.listaFavoritos.length === 0) {
      commit("incLoading");
      FavoritosAPI.getAll()
        .then(response => {
          commit("decLoading");
          commit("set_lista_favoritos", { lista: response.data });
        })
        .catch(error => {
          commit("decLoading");
          error.message = "getFavoritos: " + error.message;
          commit("setError", error);
        });
    }
  },
  refreshFavoritos({ commit }) {
    commit("incLoading");
    FavoritosAPI.getAll()
      .then(response => {
        commit("decLoading");
        commit("set_lista_favoritos", { lista: response.data });
      })
      .catch(error => {
        commit("decLoading");
        error.message = "getFavoritos: " + error.message;
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
