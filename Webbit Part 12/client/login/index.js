import angular from 'angular';
import template from './login.html';
import controller from './login.controller';

angular.module('webbit.controllers')
  .config($stateProvider => {
    $stateProvider
      .state('exterior.login', {
        url: '/login',
        template,
        controller
      });
  });
