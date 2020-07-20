import angular from 'angular';
import controller from './rating.controller';
import template from './rating.html';
import './rating.less'

const COMPONENT = 'rating';

angular.module('webbit.controllers')
    .component(COMPONENT, {
        bindings: {
            itemIds: '<',
            rating : '<',
            userVote: '<'
        },
        template,
        controller
    });

export default COMPONENT;