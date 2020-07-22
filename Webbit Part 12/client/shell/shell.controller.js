import angular from 'angular';

const CONTROLLER = 'shell';

angular.module('webbit.controllers')
  .controller(CONTROLLER, ($scope, $state, Auth, Subwebbit) => {
    $scope.user = Auth.getCurrentUser();

    $scope.logout = () => {
      Auth.logout();
      $state.go('exterior.login');
    };

    $scope.searchSubwebbits = () => Subwebbit.search({name: $scope.searchedName}).$promise;

    $scope.clearSearchBar = () => {
      $scope.searchedName = "";
    }
    
    $scope.createSubwebbit = () => {
      Subwebbit.create({name: $scope.searchedName}).$promise
        .then(({ id }) => $state.go('shell.subwebbit', { id }))
        .then($scope.clearSearchBar)
    };

    $scope.goHome = () => {
      $state.go('shell.home', {}, {reload: true});
    }
  });

export default CONTROLLER;