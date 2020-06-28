import angular from 'angular';
import template from './shell.html';
import controller from './shell.controller'
import './shell.less';

angular.module('webbit.controllers')
  .config($stateProvider => {
    $stateProvider
      .state('shell', {
        abstract: true,
        template,
        controller
      });
  });
