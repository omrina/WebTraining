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
import components from './components';
import 'lodash';
import './app.less';

angular.module('webbit', [uiRouter, 'ui.router.state.events', ngResource, ngAnimate, ngAria, ngMessages, ngMaterial, services, controllers, components])
    .config(($locationProvider, $urlRouterProvider, $httpProvider) => {
        $urlRouterProvider.otherwise('/');
        $locationProvider.html5Mode(true);
        $httpProvider.interceptors.push(function(Storage) {
            return {
              request: request => {
                request.headers.Authorization = Storage.getUser().id;

                return request;
              }
            };
        });
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