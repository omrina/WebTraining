import angular from 'angular';
import ngMaterial from 'angular-material';
import ngAnimate from 'angular-animate';
import ngAria from 'angular-aria';
import ngResource from 'angular-resource';
import ngMessages from 'angular-messages';
import uiRouter from 'angular-ui-router';
import 'angular-ui-router/release/stateEvents';
import 'angular-material/angular-material.min.css';
import 'mdi/css/materialdesignicons.css';
import services from './services';
import controllers from './controllers';
import './app.less';

angular.module('webbit', [uiRouter, 'ui.router.state.events', ngResource, ngAnimate, ngAria, ngMessages, ngMaterial, services, controllers])
    .config(($locationProvider, $urlRouterProvider, $httpProvider) => {
        $urlRouterProvider.otherwise('/');
        $locationProvider.html5Mode(true);

        $httpProvider.interceptors.push(($rootScope, $q, UserStorage) => {
            return {
              responseError: response => {
                const status = response.status;

                if (status >= 500 || status === 400) {
                  $rootScope.$broadcast('AlertError');
                }

                return $q.reject(response);
              },
              request: request => {
                const currentUser = UserStorage.getUser();

                request.headers.Authorization = currentUser
                                                ? currentUser.token
                                                : "";

                return request;
              },
            };
        });
    })
    .run(($rootScope, $state, Auth, Alert) => {
        $rootScope.$on('AlertError', () => {
          Alert.error('Something went wrong... please try again later');
        });
        
        $rootScope.$on('$stateChangeStart', (event, next) => {
            if (['exterior.login', 'exterior.signup'].includes(next.name)) {
                return;
            }

            if (!Auth.isLoggedIn()) {
                event.preventDefault();
                $state.go('exterior.login');
            }
        });
    });