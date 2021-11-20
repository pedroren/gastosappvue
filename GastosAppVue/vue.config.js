//const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin;
module.exports = {
  publicPath:
    process.env.NODE_ENV === "production"
      ? "/gastosvue/"
      : "/" /*,
    configureWebpack: {
      plugins: [new BundleAnalyzerPlugin()]
    }*/
    //,parallel: false
};
