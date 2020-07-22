import angular from 'angular';

const CONTROLLER = 'rating';

angular.module('webbit.controllers')
    .controller(CONTROLLER, function () {
        this.votingStates = Object.freeze({'DOWNVOTED': -1,'NOVOTE': 0, 'UPVOTED': 1})
        
        const vote = clickedVoteState =>
          this.voteMethod({
            vote: clickedVoteState,
            ...this.itemIds,
          })
          .then(({ rating, userVote }) => {
            this.rating = rating;
            this.userVote = userVote;
          });

        this.upvote = event => {
          event.stopPropagation();
          vote(this.votingStates.UPVOTED)
        };
        
        this.downvote = event => {
          event.stopPropagation();
          vote(this.votingStates.DOWNVOTED)
        };
    });

export default CONTROLLER;