import moment from "moment";

export default value => {
  if (value) {
    return moment(String(value)).format("DD/MM/YYYY");
  } else {
    return "";
  }
};
