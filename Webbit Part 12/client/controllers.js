import angular from 'angular';

const MODULE = 'webbit.controllers';

angular.module(MODULE, []);
require('./exterior');
require('./exterior/login');
require('./exterior/signup');
require('./shell');
require('./shell/home');
require('./shell/subwebbit');
require('./shell/thread');

export default MODULE;