import angular from 'angular';

const CONTROLLER = 'rating';

angular.module('webbit.controllers')
    .controller(CONTROLLER, function (Rating) {
        this.votingStates = Object.freeze({'DOWNVOTED': -1,'NOVOTE': 0, 'UPVOTED': 1})

        const vote = clickedVoteState =>
          Rating.vote({
            ...this.itemIds,
            voteChange: {
              previousDirection: this.currentVoteState,
              newDirection: clickedVoteState,
            },
          }).then(() => {
            this.rating +=
              clickedVoteState == this.currentVoteState
                ? clickedVoteState * -1
                : clickedVoteState - this.currentVoteState;

            this.currentVoteState =
              this.currentVoteState === clickedVoteState
                ? this.votingStates.NOVOTE
                : clickedVoteState;
          });

        
        this.$onInit = () => {
            this.currentVoteState = this.userVote;

            this.upvote = () => vote(this.votingStates.UPVOTED);
            
            this.downvote = () => vote(this.votingStates.DOWNVOTED);
        }
    });

export default CONTROLLER;