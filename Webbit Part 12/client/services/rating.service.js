import angular from "angular";

angular.module('webbit.services')
    .factory('Rating', $resource => {
        const vote = (itemId, direction) => 
            $resource(`/api/votes/`).save({itemId, direction}).$promise;

        return {
            // TODO: is needed?
            // TODO: should i make enum id directions?
            getRating: itemId =>
                $resource(`/api/votes/${itemId}`).get().$promise,

            upvote: itemId => vote(itemId, 1),

            downvote: itemId => vote(itemId, -1),

            cancelVote: itemId => vote(itemId, 0),
        };
    });