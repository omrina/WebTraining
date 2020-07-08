import angular from "angular";

angular.module("webbit.services").service("Auth", function ($resource, Storage) {
  this.signup = ({ username = "", password = "" } = {}) =>
    $resource("/auth/signup").save({ username, password }).$promise;

  this.login = ({ username = "", password = "" } = {}) => {
    return $resource("/auth/login").save({ username, password }).$promise
      .then(({id, username}) => {
        Storage.setUser({id, username});
      });
  };

  this.logout = () => {
    Storage.setUser(null);
  };

  this.getCurrentUser = () => Storage.getUser();

  this.isLoggedIn = () => !!Storage.getUser();
});
