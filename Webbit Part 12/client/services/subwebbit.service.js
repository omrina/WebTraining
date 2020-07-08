import angular from "angular";

angular.module("webbit.services").service("Subwebbit", function ($http, $resource) {
  this.search = name =>
    $resource(`/api/subwebbits/search/${name}`).query().$promise;

  this.create = newSubwebbit =>
    $http.post("/api/subwebbits", newSubwebbit)
      .then(({ data }) => data);

  this.get = id =>
    $resource(`/api/subwebbits/${id}`).get().$promise;

  this.postThread = thread =>
    $resource(`/api/subwebbits/createThread`).save(thread).$promise;

  this.getThreads = ({ id = "", index }) =>
    $resource(`/api/subwebbits/${id}/threads/${index}`).query().$promise;
});