import angular from 'angular';
import template from './signup.html';
import controller from './signup.controller';

angular.module('webbit.controllers')
  .config($stateProvider => {
    $stateProvider
      .state('exterior.signup', {
        url: '/signup',
        template,
        controller
      });
  });
