import angular from 'angular';
import controller from './add-comment.controller';
import template from './add-comment.html';
import './add-comment.less'

const COMPONENT = 'addComment';

angular.module('webbit.components')
    .component(COMPONENT, {
        bindings: {
            subwebbitId: '<',
            threadId: '<',
            parentCommentId: '<',
        },
        template,
        controller
    });

export default COMPONENT;