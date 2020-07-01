import angular from 'angular';

const CONTROLLER = 'shell';

angular.module('webbit.controllers')
  .controller(CONTROLLER, ($scope, $state, Auth, Subwebbit) => {
    $scope.searchedName = "";

    // TODO: use this to show user name in toolbar?
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
      // TODO: should clear the search bar here???
      $scope.searchedName = "";
      Subwebbit.create({name, ownerId})
        .then(newSubwebbitId => $state.go('shell.subwebbit', {id: newSubwebbitId}));
    };
  });

export default CONTROLLER;