import angular from "angular";

angular.module("webbit.services").service("Comment", function ($resource) {
    this.getAll = threadId =>
        $resource(`/api/comments/${threadId}`).query().$promise;

    this.post = comment =>
        $resource(`/api/comments`).save(comment).$promise;
});