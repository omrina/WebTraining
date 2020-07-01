import angular from 'angular';

const CONTROLLER = 'subwebbit';

angular.module('webbit.controllers')
    .controller(CONTROLLER, ($scope, $state, Subwebbit) => {
        $scope.id = $state.params.id;
        Subwebbit.get($scope.id)
            .then(subwebbit => $scope.subwebbit = subwebbit);
    });

export default CONTROLLER;