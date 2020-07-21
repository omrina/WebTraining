import angular from "angular";

angular.module("webbit.services").service("SubComment", function ($resource) {
    // this.getAll = threadId =>
        // $resource(`/api/comments/${threadId}`).query().$promise;

    this.post = comment =>
        $resource(`/api/subComments`).save(comment).$promise;
});