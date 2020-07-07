import angular from 'angular';
import controller from './threads-displayer.controller';
import template from './threads-displayer.html';
import './threads-displayer.less'

const COMPONENT = 'threadsDisplayer';

angular.module('webbit.components')
    .component(COMPONENT, {
        bindings: {
            subwebbitId: '<'
        },
        template,
        controller
    });

export default COMPONENT;