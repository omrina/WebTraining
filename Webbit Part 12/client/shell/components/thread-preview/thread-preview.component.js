import angular from 'angular';
import controller from './thread-preview.controller';
import template from './thread-preview.html';
import './thread-preview.less'

const COMPONENT = 'threadPreview';

angular.module('webbit.components')
    .component(COMPONENT, {
        bindings: {
            thread: '<'
        },
        template,
        controller
    });

export default COMPONENT;