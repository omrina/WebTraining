import angular from 'angular';

const MODULE = 'webbit.controllers';

angular.module(MODULE, []);
require('./exterior');
require('./login');
require('./signup');
require('./shell');
require('./home');
require('./subwebbit');

export default MODULE;