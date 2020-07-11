import angular from 'angular';
import {ago} from 'time-ago';

const CONTROLLER = 'threadPreview';

angular.module('webbit.controllers')
    .controller(CONTROLLER, function() {
        this.toTimeAgo = date => ago(date);
    });

export default CONTROLLER;