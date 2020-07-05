import angular from 'angular';
import template from './exterior.html';
import './exterior.less';
import './content.less';

angular.module('webbit.controllers')
  .config($stateProvider => {
    $stateProvider
      .state('exterior', {
        abstract: true,
        template
      });
  });
