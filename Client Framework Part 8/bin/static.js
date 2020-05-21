const serveStatic = require('serve-static');
const { join } = require('path');

module.exports = serveStatic(join(__dirname, '..'));
