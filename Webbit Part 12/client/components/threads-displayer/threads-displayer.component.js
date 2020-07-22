import angular from 'angular';
import controller from './threads-displayer.controller';
import template from './threads-displayer.html';
import '../thread-preview/thread-preview.component';
import '../rating/rating.component';
import './threads-displayer.less';

const COMPONENT = 'threadsDisplayer';

angular.module('webbit.controllers')
    .component(COMPONENT, {
        bindings: {
            getThreadsMethod: '<'
        },
        template,
        controller
    });

export default COMPONENT;