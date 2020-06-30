import angular from 'angular';

const CONTROLLER = 'shell';

angular.module('webbit.controllers')
  .controller(CONTROLLER, ($scope, $state, Auth, $http, $timeout, $q, $log) => {
    $scope.searchedText = "";
    $scope.user = Auth.getCurrentUser();
    // $scope.selectedSubwebbit = {};
    $scope.ba = () => {
      console.log('clicked!');
    }

    $scope.logout = () => {
      Auth.logout();
      $state.go('exterior.login');
    };

    $scope.querySearch = () => 
      $http.get(`/api/subwebbits/search/${$scope.searchedText}`)
        .then(({data}) => data);
  });

export default CONTROLLER;