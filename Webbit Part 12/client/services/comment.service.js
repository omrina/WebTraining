import angular from "angular";

angular.module("webbit.services").service("Comment", function ($http, $resource) {
    this.getAll = (subwebbitId, threadId) =>
        $resource(`/api/comments/${subwebbitId}/${threadId}`).query().$promise;

    this.post = comment =>
        $http.post(`/api/comments`, comment)
            .then(({ data }) => data);
});