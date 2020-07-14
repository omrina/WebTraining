import angular from "angular";

angular.module('webbit.services')
    .service('Rating', function($resource)  {
        this.vote = voteInfo =>
          $resource(`/api/ratings/`).save(voteInfo).$promise;
    });