import axios from "axios";
import apiglobal from "./apiglobal";
import store from "../store";

var _baseurl = apiglobal.apibaseurl + "TransImportMaps/";
var _baseurlDet = apiglobal.apibaseurl + "TransImportMapItems/";
var _baseurlTag = apiglobal.apibaseurl + "TransImportTags/";

axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";

export default {
  getAll() {
    return axios.get(
      `${_baseurl}getbyusuario?usuario=${store.getters.loggedUser}`
    );
  },
  async getById(transImportMapId) {
    return axios.get(_baseurl + "" + transImportMapId);
  },
  saveNewRecord(_record) {
    _record.UsuarioId = store.getters.usuarioId;
    return axios.post(_baseurl + "PostTransImportMap", { ..._record });
  },
  updateRecord(_record) {
    return axios.put(_baseurl + "/" + _record.TransImportMapId, { ..._record });
  },
  deleteRecord(_record) {
    return axios.delete(_baseurl + "" + _record.TransImportMapId);
  },
  getAllDet(transImportMapId) {
    return axios.get(
      `${_baseurlDet}getbymaster?TransImportMapId=${transImportMapId}`
    );
  },
  getByIdDet(transImportMapDetId) {
    return axios.get(_baseurlDet + "" + transImportMapDetId);
  },
  saveNewRecordDet(_record) {
    _record.UsuarioId = store.getters.usuarioId;
    return axios.post(_baseurlDet + "PostTransImportMapDet", { ..._record });
  },
  updateRecordDet(_record) {
    return axios.put(_baseurlDet + "/" + _record.TransImportMapDetId, {
      ..._record
    });
  },
  deleteRecordDet(_record) {
    return axios.delete(_baseurlDet + "" + _record.TransImportMapDetId);
  },
  getAllTag() {
    return axios.get(
      `${_baseurlTag}getbyusuario?usuario=${store.getters.loggedUser}`
    );
  },
  getByIdTag(transImportTagId) {
    return axios.get(_baseurlTag + "" + transImportTagId);
  },
  saveNewRecordTag(_record) {
    _record.UsuarioId = store.getters.usuarioId;
    return axios.post(_baseurlTag, { ..._record });
  },
  updateRecordTag(_record) {
    return axios.put(_baseurlTag + "/" + _record.TransImportTagId, {
      ..._record
    });
  },
  deleteRecordTag(_record) {
    return axios.delete(_baseurlTag + "" + _record.TransImportTagId);
  }
};
