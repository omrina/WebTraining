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

        $scope.post = thread => {
            Thread.post(thread)
                .then(() => $mdDialog.hide())
                .catch(() => alert('thread submission failed!'));
        };
    });

export default CONTROLLER;