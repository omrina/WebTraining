import angular from 'angular';
import template from './exterior.html';
import './exterior.less';

angular.module('webbit.controllers')
  .config($stateProvider => {
    $stateProvider
      .state('exterior', {
        abstract: true,
        template
      });
  });
