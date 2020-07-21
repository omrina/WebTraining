import angular from "angular";

angular.module("webbit.services").service("Thread", function ($resource) {
    this.get = id =>
        $resource(`/api/threads/${id}`).get().$promise;

    this.post = thread =>
        $resource(`/api/threads`).save(thread).$promise;

    this.getThreads = ({ subwebbitId = "", index }) =>
        $resource(`/api/threads/${subwebbitId}/${subwebbitId ? 'recent' : 'topRated'}/${index}`).query().$promise;

    this.delete = id => 
        $resource(`/api/threads/${id}`).delete().$promise;
});