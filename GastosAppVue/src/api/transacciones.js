import axios from "axios";
import apiglobal from "./apiglobal";
import store from "../store";

var _baseurl = apiglobal.apibaseurl + "transacciones/";

axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";
export default {
  getAll() {
    return axios.get(
      `${_baseurl}GetByCanalId?canalid=${store.getters.loggedUser}`
    );
  },
  getData(fechaDesde, fechaHasta, conceptoId, cuentaId) {
    return axios.post(_baseurl + "GetTransaccionesUsuario", {
      Usuario: store.getters.loggedUser,
      FechaDesde: fechaDesde,
      FechaHasta: fechaHasta,
      ConceptoId: conceptoId,
      CuentaId: cuentaId
    });
  },
  getDataById(id) {
    return axios.get(_baseurl + id);
  },
  getTotalConcepto(fechaDesde, fechaHasta, conceptoId) {
    return axios.post(_baseurl + "GetTotalConcepto", {
      Usuario: store.getters.loggedUser,
      FechaDesde: fechaDesde,
      FechaHasta: fechaHasta,
      ConceptoId: conceptoId
    });
  },
  getTiposTrans() {
    return axios.get(_baseurl + "gettipotrans");
  },
  getNewRecord() {
    return axios.get(_baseurl + "getnewrecord");
  },
  saveNewRecord(_record) {
    _record.UsuarioId = store.getters.usuarioId;
    return axios.post(_baseurl + "PostTransaccion", { ..._record });
  },
  updateRecord(_record) {
    return axios.put(_baseurl + "" + _record.TransaccionId, { ..._record });
  },
  deleteRecord(_record) {
    return axios.delete(`${_baseurl}${_record.TransaccionId}`);
  }
};
