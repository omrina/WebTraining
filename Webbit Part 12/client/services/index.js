import angular from 'angular';

const MODULE = 'webbit.services';

angular.module(MODULE, []);
require('./user.service');
require('./auth.service');
require('./subwebbit.service');

export default MODULE;