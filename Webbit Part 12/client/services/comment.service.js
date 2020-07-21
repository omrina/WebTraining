import angular from "angular";

angular.module("webbit.services").service("Comment", function ($resource) {
    this.getAll = threadId =>
        $resource(`/api/comments/${threadId}`).query().$promise;

    this.post = comment =>
        $resource(`/api/comments`).save(comment).$promise;

    this.vote = voteInfo =>
        $resource(`/api/comments/vote`).save(voteInfo).$promise;
});