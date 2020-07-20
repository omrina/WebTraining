import angular from 'angular';

const CONTROLLER = 'login';

angular.module('webbit.controllers')
    .controller(CONTROLLER, ($scope, $state, Auth, Alert) => {
        $scope.user = {};

        $scope.login = () => {
            Auth.login($scope.user)
                .then(() => $state.go('shell.home'))
                .catch(({ status }) => {
                    if (status === 401) {
                        Alert.error('Username or Password are invalid');
                    }
                });
        }
    });

export default CONTROLLER;