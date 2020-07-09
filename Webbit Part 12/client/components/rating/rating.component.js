import angular from 'angular';
import controller from './rating.controller';
import template from './rating.html';
import './rating.less'

const COMPONENT = 'rating';

angular.module('webbit.components')
    .component(COMPONENT, {
        bindings: {
            itemId: '<',
            rating : '<'
        },
        template,
        controller
    });

export default COMPONENT;