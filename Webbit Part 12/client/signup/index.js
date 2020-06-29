import angular from 'angular';
import template from './signup.html';
import controller from './signup.controller';
import '../exterior/content.less';

angular.module('webbit.controllers')
  .config($stateProvider => {
    $stateProvider
      .state('exterior.signup', {
        url: '/signup',
        template,
        controller
      });
  });
