import angular from "angular";
import createThreadTemplate from "./create-thread/create-thread.html";
import createThreadController from "./create-thread/create-thread.controller";
import "./create-thread/create-thread.less";
import TimeAgo from "time-ago";

const CONTROLLER = "subwebbit";

angular.module("webbit.controllers")
    .controller(CONTROLLER, ($scope, $state, $timeout, $mdDialog, Subwebbit, User) => {
      const id = $state.params.id;
      // TODO: get from server if subscribed (AND if owner)!
      $scope.toTimeAgo = (date) => TimeAgo.ago(date);

      $scope.toggleSubscribe = (clickedButton, id) => {
        const isSubscribed = $scope.subwebbit.isSubscribed;

        clickedButton.disabled = true;
        User[isSubscribed ? "unsubscribe" : "subscribe"](id).then(() => {
          $scope.subwebbit.subscribersCount += isSubscribed ? -1 : 1;
          $scope.subwebbit.isSubscribed = !isSubscribed;
          clickedButton.disabled = false;
        });
      };

      $scope.showCreateDialog = () => {
        $mdDialog
          .show({
            locals: { subwebbit: $scope.subwebbit },
            controller: createThreadController,
            template: createThreadTemplate,
          })
          .then(() => {
            // TODO: should move to the newly created thread page instead of reload?
            $timeout(() => $scope.$broadcast("CreateNewThread"), 1000);
          });
      };

      Subwebbit.get(id).then(subwebbit => {
        $scope.subwebbit = subwebbit;
      });
    }
  );

export default CONTROLLER;
