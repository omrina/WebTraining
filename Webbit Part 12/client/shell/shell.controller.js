import angular from 'angular';

const CONTROLLER = 'shell';

angular.module('webbit.controllers')
  .controller(CONTROLLER, ($scope, $state, Auth, Subwebbit) => {
    $scope.searchedName = "";
    $scope.user = Auth.getCurrentUser();

    $scope.logout = () => {
      Auth.logout();
      $state.go('exterior.login');
    };

    $scope.searchSubwebbits = name => Subwebbit.search(name);

    $scope.clearSearchBar = () => {
      $scope.searchedName = "";
    }
    
    $scope.createSubwebbit = (name, ownerId) => {
      $scope.clearSearchBar();
      Subwebbit.create({name, ownerId})
        .then(newSubwebbitId => $state.go('shell.subwebbit', {id: newSubwebbitId}));
    };

    $scope.goHome = () => {
      $state.go('shell.home', {}, {reload: true});
    }
  });

export default CONTROLLER;