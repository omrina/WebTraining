import angular from 'angular';

const MODULE = 'webbit.controllers';

angular.module(MODULE, []);
require('./exterior');
require('./login');
require('./shell');
require('./home');

export default MODULE;