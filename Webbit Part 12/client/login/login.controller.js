import angular from 'angular';

const CONTROLLER = 'login';

angular.module('webbit.controllers')
    .controller(CONTROLLER, ($scope, $state, Auth, $mdToast) => {
        $scope.user = {};

        $scope.login = () => {
            Auth.login($scope.user)
                .then(() => {
                    $state.go('shell.home');
                })
                .catch(status => {
                    status = status > 500 ? 500 : status;

                    const errors = {
                        400: 'Username and Password are invalid',
                        500: 'An error has occurred, please try later'
                    };

                    $mdToast.show(
                        $mdToast.simple()
                            .textContent(errors[status])
                            .hideDelay(4000)
                    );
                });
        }
    });

export default CONTROLLER;