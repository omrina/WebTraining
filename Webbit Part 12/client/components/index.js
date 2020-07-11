import angular from 'angular';

const MODULE = 'webbit.components';

angular.module(MODULE, []);
require('./threads-displayer/threads-displayer.component');
require('./thread-preview/thread-preview.component');
require('./comment/comment.component');
require('./add-comment/add-comment.component');
require('./rating/rating.component');

export default MODULE;