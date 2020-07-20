import angular from 'angular';
import template from './thread.html';
import controller from './thread.controller';
import './components/add-comment/add-comment.component'
import './components/comment/comment.component';
import './thread.less';

angular.module('webbit.controllers')
  .config($stateProvider => {
    $stateProvider
      .state('shell.thread', {
        url: '/subwebbits/{subwebbitId}/threads/{threadId}',
        template,
        controller
      });
  });