import angular from 'angular';
import template from './subwebbit.html';
import controller from './subwebbit.controller';
import './subwebbit.less';

angular.module('webbit.controllers')
  .config($stateProvider => {
    $stateProvider
      .state('shell.subwebbit', {
        url: '/subwebbits/{id}',
        template,
        controller
      });
  });