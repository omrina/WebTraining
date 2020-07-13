import angular from "angular";

angular.module("webbit.services").service("Thread", function ($resource) {
    this.get = (subwebbitId, threadId) =>
        $resource(`/api/threads/${subwebbitId}/${threadId}`).get().$promise;

    this.post = thread =>
        $resource(`/api/threads`).save(thread).$promise;

    this.getThreads = ({ subwebbitId = "", index }) =>
        $resource(`/api/threads/${subwebbitId}/${subwebbitId ? 'recent' : 'topRated'}/${index}`).query().$promise;

    this.delete = (subwebbitId, threadId) => 
        $resource(`/api/threads/${subwebbitId}/${threadId}`).delete().$promise;
});