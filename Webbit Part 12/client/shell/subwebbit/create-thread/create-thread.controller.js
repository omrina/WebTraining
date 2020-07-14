import angular from 'angular';

const CONTROLLER = 'createThread';

angular.module('webbit.controllers')
    .controller(CONTROLLER, ($scope, $mdDialog, Auth, Thread, subwebbit) => {
        $scope.subwebbit = subwebbit;

        $scope.thread = {
            author: Auth.getCurrentUser().username,
            subwebbitId: $scope.subwebbit.id
        };

        $scope.closeDialog = function () {
            $mdDialog.cancel();
        };

        $scope.post = () => {
            Thread.post($scope.thread)
                .then(() => $mdDialog.hide());
        };
    });

export default CONTROLLER;