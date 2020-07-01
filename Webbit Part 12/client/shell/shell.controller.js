import angular from 'angular';

const CONTROLLER = 'shell';

angular.module('webbit.controllers')
  .controller(CONTROLLER, ($scope, $state, Auth, Subwebbit) => {
    $scope.searchedName = "";

    // TODO: use this to show usr name in toolbar?
    $scope.user = Auth.getCurrentUser();

    $scope.logout = () => {
      Auth.logout();
      $state.go('exterior.login');
    };

    $scope.searchSubwebbits = () => Subwebbit.search($scope.searchedName);
  });

export default CONTROLLER;