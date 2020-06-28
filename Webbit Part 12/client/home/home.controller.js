import angular from 'angular';

const CONTROLLER = 'home';

angular.module('webbit.controllers')
    .controller(CONTROLLER, ($scope, $state, Auth) => {
        $scope.user = Auth.getCurrentUser();

        $scope.logout = () => {
            Auth.logout();
            $state.go('exterior.login');
        };
    });

export default CONTROLLER;