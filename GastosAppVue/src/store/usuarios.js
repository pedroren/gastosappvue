import Vue from "vue";
import Vuex from "vuex";
import UsuariosAPI from "../api/usuarios";

Vue.use(Vuex);

const state = {
  usuario: null,
  usuarioId: 0
};

const getters = {
  loggedUser: state => state.usuario,
  isUserLogged: state => state.usuario !== null,
  usuarioId: state => state.usuarioId
};

const mutations = {
  setUsuario(state, { userLogin }) {
    state.usuario = userLogin;
  },
  setUsuarioId(state, { id }) {
    state.usuarioId = id;
  }
};

const actions = {
  setUsuario({ commit }, payload) {
    // Test user
    commit("setUsuario", { userLogin: payload.usuario });
    commit("incLoading");
    UsuariosAPI.getCurrentUsuario()
      .then(response => {
        commit("decLoading");
        commit("setUsuarioId", { id: response.data.UsuarioId });
      })
      .catch(error => {
        commit("decLoading");
        error.message = "getCurrentUsuario: " + error.message;
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
