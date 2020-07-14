import angular from 'angular';

const CONTROLLER = 'signup';

angular.module('webbit.controllers')
    .controller(CONTROLLER, ($scope, $state, Auth, Alert) => {
        $scope.user = {};

        $scope.signup = () => {
            if ($scope.user.password !== $scope.user.repeatedPassword) {
                Alert.error('The repeated password doesn\'t match...');

                return;
            }

            Auth.signup($scope.user)
                .then(() => {
                    Alert.success('Thanks for signing up to Webbit!');
                    $state.go('exterior.login');
                })
                .catch(({ status }) => {
                    if (status === 409) {
                      Alert.error("This username is already taken :/");
                    }
                });
        }
    });

export default CONTROLLER;