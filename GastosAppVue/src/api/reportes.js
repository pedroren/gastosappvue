import axios from "axios";
import apiglobal from "./apiglobal";
import store from "../store";

var _baseurl = apiglobal.apibaseurl + "reportes/";

axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";
export default {
  getSumConcepto(fechaDesde, fechaHasta, cuentaId) {
    // return axios.post(_baseurl + 'getbyusuario', { usuario: 'invitado' })
    return axios.post(_baseurl + "GetSumConcepto", {
      Usuario: store.getters.loggedUser,
      FechaDesde: fechaDesde,
      FechaHasta: fechaHasta,
      CuentaId: cuentaId
    });
  },
  getSumCashFlow(fechaDesde, fechaHasta) {
    // return axios.post(_baseurl + 'getbyusuario', { usuario: 'invitado' })
    return axios.post(_baseurl + "GetSumCashFlow", {
      Usuario: store.getters.loggedUser,
      FechaDesde: fechaDesde,
      FechaHasta: fechaHasta
    });
  },
  getHistConcepto(fechaDesde, fechaHasta, conceptoId) {
    // return axios.post(_baseurl + 'getbyusuario', { usuario: 'invitado' })
    return axios.post(_baseurl + "GetHistAnoMes", {
      Usuario: store.getters.loggedUser,
      FechaDesde: fechaDesde,
      FechaHasta: fechaHasta,
      ConceptoId: conceptoId
    });
  },
  getRepPresupuesto(presupuestoId) {
    return axios.get(
      _baseurl + "GetRepPresupuesto?PresupuestoId=" + presupuestoId
    );
  }
};
