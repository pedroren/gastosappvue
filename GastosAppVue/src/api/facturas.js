import axios from "axios";
import apiglobal from "./apiglobal";
import store from "../store";

var _baseurl = apiglobal.apibaseurl + "facturas";

axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";

export default {
  getAll() {
    // return axios.post(_baseurl + 'getbyusuario', { usuario: store.getters.loggedUser }, axiosConfig)
    return axios.get(`${_baseurl}?usuario=${store.getters.loggedUser}`);
  },
  getDataById(id) {
    return axios.get(_baseurl + "/" + id);
  },
  saveNewRecord(_record) {
    _record.UsuarioId = store.getters.usuarioId;
    return axios.post(_baseurl + "", { ..._record });
  },
  updateRecord(_record) {
    return axios.put(_baseurl + "/" + _record.FacturaId, { ..._record });
  },
  deleteRecord(_record) {
    return axios.delete(_baseurl + "/" + _record.FacturaId);
  },
  getProxFechaFact(_record) {
    console.log(_record);
    return axios.post(_baseurl + "/GetProxFechaFact", { ..._record });
  }
};
