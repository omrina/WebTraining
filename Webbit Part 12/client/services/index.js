import angular from 'angular';

const MODULE = 'webbit.services';

angular.module(MODULE, []);
require('./user.service');
require('./auth.service');
require('./storage.service');
require('./subwebbit.service');
require('./thread.service');
require('./rating.service');
require('./comment.service');

export default MODULE;