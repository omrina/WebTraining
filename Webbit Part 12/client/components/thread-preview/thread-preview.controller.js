import angular from 'angular';
import TimeAgo from 'time-ago';

const CONTROLLER = 'threadPreview';

angular.module('webbit.controllers')
    .controller(CONTROLLER, function() {
        this.toTimeAgo = date => TimeAgo.ago(date);
    });

export default CONTROLLER;