import angular from "angular";

angular.module("webbit.services").service("Auth", function ($resource, UserStorage) {
  this.login = ({ username = "", password = "" } = {}) => {
    return $resource("/auth/login").save({ username, password }).$promise
      .then(({token, username}) => UserStorage.setUser({token, username}));
  };

  this.logout = () => {
    $resource("/auth/logout").save().$promise
      .then(() => UserStorage.setUser(null));
  };

  this.getCurrentUser = () => UserStorage.getUser();

  this.isLoggedIn = () => !!UserStorage.getUser();
});
