import angular from 'angular';
import { ago } from 'time-ago';
  
const CONTROLLER = 'comment';

angular.module('webbit.controllers')
    .controller(CONTROLLER, function ($scope) {
        this.isReplyVisible = false;

        this.toTimeAgo = date => ago(date);
        
        this.toggleReply = () => {
            this.isReplyVisible = !this.isReplyVisible;
        }

        this.isRootComment = () => !this.parentCommentId;

        $scope.$on('PostedComment', () =>  {this.isReplyVisible = false;})
    });

export default CONTROLLER;