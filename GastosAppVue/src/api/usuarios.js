import axios from "axios";
import apiglobal from "./apiglobal";
import store from "../store";

var _baseurl = apiglobal.apibaseurl + "usuarios";

axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";

export default {
  getAll() {
    // return axios.post(_baseurl + 'getbyusuario', { usuario: store.getters.loggedUser }, axiosConfig)
    return axios.get(_baseurl + "");
  },
  getCurrentUsuario() {
    return axios.get(`${_baseurl}/getbyusuario/${store.getters.loggedUser}`);
    //return axios.get(_baseurl + 'getbyusuario/' + store.getters.loggedUser)
  }
};
