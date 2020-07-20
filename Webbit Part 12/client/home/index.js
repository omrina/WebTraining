import angular from 'angular';
import component from '../components/threads-displayer/threads-displayer.component';

angular.module('webbit.controllers')
  .config($stateProvider => {
    $stateProvider
      .state('shell.home', {
        url: '/',
        component
      });
  });
