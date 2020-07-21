import angular from "angular";
import createThreadTemplate from "./create-thread/create-thread.html";
import createThreadController from "./create-thread/create-thread.controller";
import "./create-thread/create-thread.less";
import TimeAgo from "time-ago";

const CONTROLLER = "subwebbit";

angular.module("webbit.controllers")
    .controller(CONTROLLER, ($scope, $state, $mdDialog, Subwebbit, Alert) => {
      $scope.toTimeAgo = date => TimeAgo.ago(date);

      Subwebbit.get($state.params.id)
        .then(subwebbit => {
          $scope.subwebbit = subwebbit;
        });

      $scope.toggleSubscribe = clickedButton => {
        const isSubscribed = $scope.subwebbit.isSubscribed;
        clickedButton.disabled = true;

        Subwebbit[isSubscribed ? "unsubscribe" : "subscribe"]($scope.subwebbit.id).then(() => {
          $scope.subwebbit.subscribersCount += isSubscribed ? -1 : 1;
          $scope.subwebbit.isSubscribed = !isSubscribed;
          clickedButton.disabled = false;
        });
      };

      $scope.delete = () => 
        Subwebbit.delete($scope.subwebbit.id)
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
