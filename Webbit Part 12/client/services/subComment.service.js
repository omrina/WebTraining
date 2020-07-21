import angular from "angular";

angular.module("webbit.services").service("SubComment", function ($resource) {
    this.vote = voteInfo =>
        $resource(`/api/subComments/vote`).save(voteInfo).$promise;

    this.post = comment =>
        $resource(`/api/subComments`).save(comment).$promise;
});