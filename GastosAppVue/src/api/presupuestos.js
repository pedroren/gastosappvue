import axios from "axios";
import apiglobal from "./apiglobal";
import store from "../store";

var _baseurl = apiglobal.apibaseurl + "presupuestos";
var _baseurlDet = apiglobal.apibaseurl + "presupuestoDets";

axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";

export default {
  getAll() {
    return axios.get(`${_baseurl}?usuario=${store.getters.loggedUser}`);
  },
  getAllDesc() {
    return axios.get(`${_baseurl}?usuario=${store.getters.loggedUser}`);
  },
  async getById(presupuestoId) {
    return axios.get(_baseurl + "/" + presupuestoId);
  },
  saveNewRecord(_record) {
    _record.UsuarioId = store.getters.usuarioId;
    return axios.post(_baseurl + "", { ..._record });
  },
  updateRecord(_record) {
    return axios.put(_baseurl + "/" + _record.PresupuestoId, { ..._record });
  },
  deleteRecord(_record) {
    return axios.delete(_baseurl + "/" + _record.PresupuestoId);
  },
  getAllDet(presupuestoId) {
    return axios.get(`${_baseurlDet}?PresupuestoId=${presupuestoId}`);
  },
  getByIdDet(presupuestoDetId) {
    return axios.get(_baseurlDet + "/" + presupuestoDetId);
  },
  saveNewRecordDet(_record) {
    _record.UsuarioId = store.getters.usuarioId;
    return axios.post(_baseurlDet + "", { ..._record });
  },
  updateRecordDet(_record) {
    return axios.put(_baseurlDet + "/" + _record.PresupuestoDetId, {
      ..._record
    });
  },
  deleteRecordDet(_record) {
    return axios.delete(_baseurlDet + "/" + _record.PresupuestoDetId);
  }
};
