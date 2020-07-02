import angular from 'angular';
import createThreadTemplate from './create-thread/create-thread.html';
import createThreadController from './create-thread/create-thread.controller';
import './create-thread/create-thread.less';

const CONTROLLER = 'subwebbit';

angular.module('webbit.controllers')
    .controller(CONTROLLER, ($scope, $state, $mdDialog, Subwebbit) => {
        const id = $state.params.id;
        // TODO: get from server if subscribed (AND if owner)!
        $scope.isSubscribed = true;
        Subwebbit.get(id)
            .then(subwebbit => $scope.subwebbit = subwebbit);

        $scope.showCreateDialog = () => {
            $mdDialog.show({
                    locals: {targetSubwebbit: $scope.subwebbit},
                    controller: createThreadController,
                    template: createThreadTemplate,
                })
                .then(function (answer) {
                    $scope.status = 'You said the information was "' + answer + '".';
                }, function () {
                    $scope.status = 'You cancelled the dialog.';
                });
        };
    });

export default CONTROLLER;