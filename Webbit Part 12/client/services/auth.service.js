import angular from "angular";

angular.module("webbit.services").service("Auth", function ($http, Storage) {
  this.signup = ({ username = "", password = "" } = {}) =>
    $http.post("/auth/signup", { username, password });

  this.login = ({ username = "", password = "" } = {}) => {
    return $http
      .post("/auth/login", { username, password })
      .then(({ data }) => {
        Storage.setUser(data);
      });
  };

  this.logout = () => {
    Storage.setUser(null);
  };

  this.getCurrentUser = () => Storage.getUser();

  this.isLoggedIn = () => !!Storage.getUser();
});
