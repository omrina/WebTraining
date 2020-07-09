import angular from 'angular';
import template from './thread.html';
import controller from './thread.controller';
import './thread.less';

angular.module('webbit.controllers')
  .config($stateProvider => {
    $stateProvider
      .state('shell.thread', {
        params: {
          thread: null
        },
        url: '/subwebbits/{subwebbitId}/threads/{threadId}',
        template,
        controller
      });
  });