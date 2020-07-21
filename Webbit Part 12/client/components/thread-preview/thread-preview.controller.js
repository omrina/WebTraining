import angular from 'angular';
import {ago} from 'time-ago';

const CONTROLLER = 'threadPreview';

angular.module('webbit.controllers')
    .controller(CONTROLLER, function($state, Thread, Alert) {
        this.toTimeAgo = date => ago(date);

        this.voteMethod = Thread.vote;

        this.delete = event => {
          event.stopPropagation();
          Thread.delete(this.thread.id)
            .then(() =>
              $state.go(
                "shell.subwebbit",
                { id: this.thread.subwebbitId },
                { reload: true }
              )
            )
            .then(() => Alert.success("The post has been deleted."));
        }
    });

export default CONTROLLER;