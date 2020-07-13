import angular from 'angular';

const MODULE = 'webbit.components';

angular.module(MODULE, []);

require('./shell/components/threads-displayer/threads-displayer.component');
require('./shell/components/thread-preview/thread-preview.component');
require('./shell/components/rating/rating.component');
require('./shell/thread/components/add-comment/add-comment.component');
require('./shell/thread/components/comment/comment.component');

export default MODULE;