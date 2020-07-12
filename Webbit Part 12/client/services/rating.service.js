import angular from "angular";

angular.module('webbit.services')
    .factory('Rating', $resource => {
        // TODO: change to service!?
        return {
            vote: voteInfo => $resource(`/api/ratings/`).save(voteInfo).$promise,
        };
    });