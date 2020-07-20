import angular from "angular";

angular.module("webbit.services").service("User", function ($resource) {
  this.signup = ({ username = "", password = "" } = {}) =>
    $resource("/api/users/signup").save({ username, password }).$promise;
});
