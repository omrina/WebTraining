import angular from "angular";
import createThreadTemplate from "./create-thread/create-thread.html";
import createThreadController from "./create-thread/create-thread.controller";
import "./create-thread/create-thread.less";
import TimeAgo from "time-ago";

const CONTROLLER = "subwebbit";

angular.module("webbit.controllers")
    .controller(CONTROLLER, ($scope, $state, $mdDialog, Subwebbit, Thread, Alert) => {
      $scope.toTimeAgo = date => TimeAgo.ago(date);

      $scope.getThreadsMethod = index => Thread.getRecent($scope.subwebbit.id, index);

      Subwebbit.get({id: $state.params.id}).$promise
        .then(subwebbit => {
          $scope.subwebbit = subwebbit;
        });

      $scope.toggleSubscribe = clickedButton => {
        const isSubscribed = $scope.subwebbit.isSubscribed;
        clickedButton.disabled = true;

        Subwebbit[isSubscribed ? "unsubscribe" : "subscribe"]({id: $scope.subwebbit.id}).$promise
          .then(() => {
            $scope.subwebbit.subscribersCount += isSubscribed ? -1 : 1;
            $scope.subwebbit.isSubscribed = !isSubscribed;
            clickedButton.disabled = false;
        });
      };

      $scope.delete = () => 
        Subwebbit.delete({id: $scope.subwebbit.id}).$promise
          .then(() => $state.go('shell.home'))
          .then(() => Alert.success(`The subwebbit ${$scope.subwebbit.name} has been deleted.`));
      

      $scope.showThreadCreationDialog = () => {
        $mdDialog
          .show({
            locals: { subwebbit: $scope.subwebbit },
            controller: createThreadController,
            template: createThreadTemplate,
          })
          .then(() => $scope.$broadcast("CreatedNewThread"));
      };
    }
  );

export default CONTROLLER;
