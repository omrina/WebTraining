import angular from 'angular';
import template from './home.html';
import controller from './home.controller';
import '../components/threads-displayer/threads-displayer.component';

angular.module('webbit.controllers')
  .config($stateProvider => {
    $stateProvider
      .state('shell.home', {
        url: '/',
        template,
        controller
      });
  });
