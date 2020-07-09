import angular from 'angular';

const MODULE = 'webbit.components';

angular.module(MODULE, []);
require('./threads-displayer/threads-displayer.component');
require('./rating/rating.component');

export default MODULE;