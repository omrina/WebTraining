import angular from "angular";

const CONTROLLER = "thread";

angular.module("webbit.controllers")
    .controller(CONTROLLER, ($scope, $stateParams) => {
      $scope.thread = $stateParams.thread;
    }
  );

export default CONTROLLER;
