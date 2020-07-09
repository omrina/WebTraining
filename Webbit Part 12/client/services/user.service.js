import angular from "angular";

// TODO: rename to subscribtion service? OR to /subwebbits/subscribe?
angular.module("webbit.services").service("User", function ($resource) {
  this.subscribe = subwebbitId =>
    $resource(`api/users/subscribe/${subwebbitId}`).save().$promise;

  this.unsubscribe = subwebbitId =>
    $resource(`api/users/unsubscribe/${subwebbitId}`).save().$promise;
});
