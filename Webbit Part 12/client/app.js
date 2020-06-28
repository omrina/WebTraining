import angular from 'angular';
import ngMaterial from 'angular-material';
import ngAnimate from 'angular-animate';
import ngAria from 'angular-aria';
import ngResource from 'angular-resource';
import uiRouter from 'angular-ui-router';
import 'angular-material/angular-material.min.css';
import 'mdi/css/materialdesignicons.css';

import services from './services';
import controllers from './controllers';

angular.module('webbit', [uiRouter, ngResource, ngAnimate, ngAria, ngMaterial, services, controllers])
    .config(($locationProvider, $urlRouterProvider) => {
        $urlRouterProvider.otherwise('/');
        $locationProvider.html5Mode(true);
    })
    .run(($rootScope, $state, Auth) => {
        $rootScope.$on('$stateChangeStart', (event, next) => {
            if (next.name === 'exterior.login') {
                return;
            }

            if (!Auth.isLoggedIn()) {
                event.preventDefault();
                $state.go('exterior.login');
            }
        });
    });
