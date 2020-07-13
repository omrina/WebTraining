import angular from 'angular';

const CONTROLLER = 'signup';

angular.module('webbit.controllers')
    .controller(CONTROLLER, ($scope, $state, Auth, $mdToast) => {
        $scope.user = {};

        $scope.signup = () => {
            if ($scope.user.password !== $scope.user.repeatedPassword) {
                $mdToast.show(
                    $mdToast.simple()
                        .textContent('The repeated password doesn\'t match...')
                        .hideDelay(3500)
                );

                return;
            }

            Auth.signup($scope.user)
                .then(() => {
                    $mdToast.show(
                        $mdToast.simple()
                            .textContent('Thanks for signing up to Webbit!')
                            .hideDelay(3000)
                    );

                    $state.go('exterior.login');
                })
                .catch(({status}) => {
                    status = status > 500 ? 500 : status;

                    const errors = {
                        400: 'Something\'s not right... but it\'s not your fault! Please try again later',
                        409: 'This username is already taken :/',
                        500: 'An error has occurred, please try later'
                    };

                    $mdToast.show(
                        $mdToast.simple()
                            .textContent(errors[status])
                            .hideDelay(3500)
                    );
                });
        }
    });

export default CONTROLLER;