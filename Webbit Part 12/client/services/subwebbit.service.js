import angular from "angular";

angular.module("webbit.services").service("Subwebbit", function ($resource) {
  this.search = name =>
    $resource(`/api/subwebbits/search/${name}`).query().$promise;

  this.create = name =>
    $resource(`/api/subwebbits/${name}`).save().$promise;

  this.get = id =>
    $resource(`/api/subwebbits/${id}`).get().$promise;

  this.delete = id => 
    $resource(`/api/subwebbits/${id}`).delete().$promise;

  this.subscribe = subwebbitId =>
    $resource(`api/subwebbits/${subwebbitId}/subscribe`).save().$promise;

  this.unsubscribe = subwebbitId =>
    $resource(`api/subwebbits/${subwebbitId}/unsubscribe`).save().$promise;
});