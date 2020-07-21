import angular from 'angular';

const CONTROLLER = 'rating';

angular.module('webbit.controllers')
    .controller(CONTROLLER, function (Rating) {
        this.votingStates = Object.freeze({'DOWNVOTED': -1,'NOVOTE': 0, 'UPVOTED': 1})
        
        //TODO: check onInit??? no bindings before it
        this.$onInit = () => {
            this.currentVoteState = this.userVote;
            
            const vote = clickedVoteState =>
              this.voteMethod({
                vote: clickedVoteState,
                ...this.itemIds,
              })
              .then(({ rating, userVote }) => {
                this.rating = rating;
                this.currentVoteState = userVote;
              });

            this.upvote = event => {
              event.stopPropagation();
              vote(this.votingStates.UPVOTED)
            };
            
            this.downvote = event => {
              event.stopPropagation();
              vote(this.votingStates.DOWNVOTED)
            };
        }
    });

export default CONTROLLER;