import angular from 'angular';
import {ago} from 'time-ago';

const CONTROLLER = 'threadPreview';

angular.module('webbit.controllers')
    .controller(CONTROLLER, function($state, Thread) {
        this.toTimeAgo = date => ago(date);

        this.delete = () => 
            Thread.delete(this.thread.subwebbitId, this.thread.id)
                .then(() => {
                    $state.go(
                      "shell.subwebbit",
                      { id: this.thread.subwebbitId },
                      { reload: true }
                    );
                });
    });

export default CONTROLLER;