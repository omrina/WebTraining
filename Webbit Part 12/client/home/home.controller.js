import angular from "angular";

const CONTROLLER = "home";

angular.module("webbit.controllers")
    .controller(CONTROLLER, ($scope, Thread) => {
      $scope.getThreadsMethod = Thread.getTopRated;
    }
  );

export default CONTROLLER;
