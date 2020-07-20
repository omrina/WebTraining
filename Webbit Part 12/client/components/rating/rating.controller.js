import angular from 'angular';

const CONTROLLER = 'rating';

angular.module('webbit.controllers')
    .controller(CONTROLLER, function (Rating) {
        this.votingStates = Object.freeze({'DOWNVOTED': -1,'NOVOTE': 0, 'UPVOTED': 1})

        const vote = clickedVoteState =>
          Rating.vote({
            ...this.itemIds,
            voteChange: {
              // TODO: dont send previous- the server should know by the userId
              previousDirection: this.currentVoteState,
              newDirection: clickedVoteState,
            },
          }).then(() => {
            // TODO: server should returnn the new rating value and the new user's vote
            this.rating +=
              clickedVoteState == this.currentVoteState
                ? clickedVoteState * -1
                : clickedVoteState - this.currentVoteState;

            this.currentVoteState =
              this.currentVoteState === clickedVoteState
                ? this.votingStates.NOVOTE
                : clickedVoteState;
          });
        
          //TODO: check onInit??? no bindings before it
        this.$onInit = () => {
            this.currentVoteState = this.userVote;

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