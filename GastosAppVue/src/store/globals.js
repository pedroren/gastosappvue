import Vue from "vue";
import Vuex from "vuex";
import moment from "moment";

Vue.use(Vuex);

const state = {
  count: 0,
  loading: 0,
  todayDate: null,
  firstDate: null,
  datesLoaded: false,
  error: null,
  message: null
};

const getters = {
  evenOrOdd: state => (state.count % 2 === 0 ? "even" : "odd"),
  loading: state => state.loading,
  todayDate: state => state.todayDate,
  firstDate: state => state.firstDate,
  todayDateString: state => state.todayDate.toISOString().substr(0, 10),
  firstDateString: state => state.firstDate.toISOString().substr(0, 10),
  datesLoaded: state => state.datesLoaded,
  error: state => state.error,
  message: state => state.message
};

const mutations = {
  incLoading(state) {
    state.loading++;
  },
  decLoading(state) {
    state.loading--;
  },
  setTodayDate(state, { date }) {
    state.todayDate = date;
  },
  setFirstDate(state, { date }) {
    state.firstDate = date;
    state.datesLoaded = true;
  },
  setError(state, payload) {
    state.error = payload;
  },
  clearError(state) {
    state.error = null;
  },
  setMessage(state, payload) {
    state.message = payload;
  },
  clearMessage(state) {
    state.message = null;
  }
};

const actions = {
  incLoading: ({ commit }) => commit("incLoading"),
  decLoading: ({ commit }) => commit("decLoading"),
  initializeDates({ commit }) {
    var todayDate = moment()
      .startOf("day")
      .toDate();
    commit("setTodayDate", { date: todayDate });
    var firstDate = moment()
      .startOf("month")
      .toDate();
    commit("setFirstDate", { date: firstDate });
  },
  setError({ commit }, payload) {
    commit("setError", payload);
  },
  clearError({ commit }) {
    commit("clearError");
  },
  setMessage({ commit }, payload) {
    commit("setMessage", payload);
  },
  clearMessage({ commit }) {
    commit("clearMessage");
  }
};

export default {
  state,
  getters,
  mutations,
  actions
};
