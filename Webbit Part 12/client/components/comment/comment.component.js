import angular from 'angular';
import controller from './comment.controller';
import template from './comment.html';
import './comment.less'

const COMPONENT = 'comment';

angular.module('webbit.components')
    .component(COMPONENT, {
        bindings: {
            isRootComment: '<',
            comment: '<',
        },
        template,
        controller
    });

export default COMPONENT;