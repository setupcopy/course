const { createProxyMiddleware } = require('http-proxy-middleware')
module.exports = function (app) {
  app.use(
    createProxyMiddleware('/auth', { 
      target: 'http://localhost:5000', 
      changeOrigin: true
    }),
    createProxyMiddleware('/api', { 
      target: 'http://localhost:5050', 
      changeOrigin: true
    })
  )
}