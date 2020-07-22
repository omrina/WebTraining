import angular from "angular";

angular.module("webbit.services").service("Thread", function ($resource) {
    this.get = id =>
        $resource(`/api/threads/${id}`).get().$promise;

    this.post = thread =>
        $resource(`/api/threads`).save(thread).$promise;

    this.getTopRated = index =>
        $resource(`/api/threads/topRated/${index}`).query().$promise

    this.getRecent = (subwebbitId = "", index) =>
        $resource(`/api/threads/${subwebbitId}/recent/${index}`).query().$promise;

    this.delete = id => 
        $resource(`/api/threads/${id}`).delete().$promise;
    
    this.vote = voteInfo =>
        $resource(`/api/threads/vote`).save(voteInfo).$promise;
});