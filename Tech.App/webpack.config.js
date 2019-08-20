module.exports = {
  entry: '/vue/main.js',
  output: {
    path: path.resolve(__dirname, './dist'),
    publicPath: '/dist/',
    filename: 'build.js'
  },
  module: {
    // add Babel here if needed
  },
  resolve: {
    alias: {
      './vue-brw.js': './node_modules/vue/dist/vue.esm.browser.js'
    }
  }
};
