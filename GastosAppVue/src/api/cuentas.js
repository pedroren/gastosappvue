import axios from "axios";
import apiglobal from "./apiglobal";
import store from "../store";

var _baseurl = apiglobal.apibaseurl + "cuentas";
var _baseurlTP = apiglobal.apibaseurl + "tipocuentas";
var _baseurlMon = apiglobal.apibaseurl + "monedas";

axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";
export default {
  getAll() {
    // return axios.post(_baseurl + 'getbyusuario', { usuario: 'invitado' })
    return axios.get(`${_baseurl}?usuario=${store.getters.loggedUser}`);
  },
  getTipos() {
    return axios.get(_baseurlTP + "");
  },
  getMonedas() {
    return axios.get(`${_baseurlMon}?usuario=${store.getters.loggedUser}`);
  },
  saveNewRecord(_record) {
    _record.UsuarioId = store.getters.usuarioId;
    return axios.post(_baseurl + "", { ..._record });
  },
  updateRecord(_record) {
    return axios.put(_baseurl + "/" + _record.CuentaId, { ..._record });
  },
  deleteRecord(_record) {
    return axios.delete(_baseurl + "/" + _record.CuentaId);
  }
};
