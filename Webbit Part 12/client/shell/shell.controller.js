import angular from 'angular';

const CONTROLLER = 'shell';

angular.module('webbit.controllers')
  .controller(CONTROLLER, ($scope, $state, Auth, $http, $timeout, $q, $log) => {
    $scope.searchText = "";
    $scope.user = Auth.getCurrentUser();
    $scope.selectedItemModel = {};

    $scope.logout = () => {
      Auth.logout();
      $state.go('exterior.login');
    };

    $scope.querySearch = () => 
      $http.get(`/api/subwebbits/search/${$scope.searchText}`)
        .then(({data}) => data);
  });

export default CONTROLLER;