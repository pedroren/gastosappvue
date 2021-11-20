import axios from "axios";
import apiglobal from "./apiglobal";
import store from "../store";

var _baseurl = apiglobal.apibaseurl + "conceptos/";

axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";

/* let axiosConfig = {
  headers: {
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE. OPTIONS',
    'Access-Control-Allow-Credentials': true,
    'Content-Type': 'application/json;charset=UTF-8'
  }
} */
export default {
  getAll() {
    // return axios.post(_baseurl + 'getbyusuario', { usuario: store.getters.loggedUser }, axiosConfig)
    return axios.get(`${_baseurl}?usuario=${store.getters.loggedUser}`);
  },
  saveNewRecord(_record) {
    _record.UsuarioId = store.getters.usuarioId;
    return axios.post(_baseurl + "", { ..._record });
  },
  updateRecord(_record) {
    return axios.put(_baseurl + "/" + _record.ConceptoId, { ..._record });
  },
  deleteRecord(_record) {
    return axios.delete(_baseurl + "/" + _record.ConceptoId);
  }
};
