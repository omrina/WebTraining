import angular from 'angular';
import template from './home.html';
import controller from './home.controller';

angular.module('webbit.controllers')
  .config($stateProvider => {
    $stateProvider
      .state('shell.home', {
        url: '/',
        template,
        controller
      });
  });
