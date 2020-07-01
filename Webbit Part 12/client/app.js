import angular from 'angular';
import ngMaterial from 'angular-material';
import ngAnimate from 'angular-animate';
import ngAria from 'angular-aria';
import ngResource from 'angular-resource';
import ngMessages from 'angular-messages';
import uiRouter from 'angular-ui-router';
import stateEvents from 'angular-ui-router/release/stateEvents';
import 'angular-material/angular-material.min.css';
import 'mdi/css/materialdesignicons.css';
import services from './services';
import controllers from './controllers';
import 'lodash';

angular.module('webbit', [uiRouter, 'ui.router.state.events', ngResource, ngAnimate, ngAria, ngMessages, ngMaterial, services, controllers])
    .config(($locationProvider, $urlRouterProvider) => {
        $urlRouterProvider.otherwise('/');
        $locationProvider.html5Mode(true);
    })
    .run(($rootScope, $state, Auth) => {
        $rootScope.$on('$stateChangeStart', (event, next) => {
            if (_.includes(['exterior.login', 'exterior.signup'], next.name)) {
                return;
            }

            if (!Auth.isLoggedIn()) {
                event.preventDefault();
                $state.go('exterior.login');
            }
        });
    });
