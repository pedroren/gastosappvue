// import config from '../../config'

let host = "pedrorenapp1.mooo.com";
let apiport = "8081";
let apipath = "/";
if (process.env.NODE_ENV !== "production") {
  host = "localhost";
  apiport = "50632";
  apipath = "/";
}

// console.log(apiport)

const apibaseurl = "http://" + host + ":" + apiport + apipath + "api/";
//const apibaseurl = 'http://pedrorenapp1.mooo.com/gastosmvc/api/'

console.log(apibaseurl);
console.log(process.env.NODE_ENV);

export default {
  apibaseurl
};
