const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT
  ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`
  : env.ASPNETCORE_URLS
    ? env.ASPNETCORE_URLS.split(';')[0]
    : 'https://localhost:7218';

const PROXY_CONFIG = [
  {
    context: [
      "/api", // Add all your API routes here
    ],
    target,
    secure: false,
    changeOrigin: true
  }
];

module.exports = PROXY_CONFIG;

