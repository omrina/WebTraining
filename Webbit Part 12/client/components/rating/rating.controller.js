import angular from 'angular';

const CONTROLLER = 'rating';

angular.module('webbit.controllers')
    .controller(CONTROLLER, function (Rating) {
        this.$onInit = () => {
            this.upvote = event => {
                event.stopPropagation();
                Rating.upvote(this.itemId);
            };
            this.downvote = event => {
                event.stopPropagation();
                Rating.downvote(this.itemId);
            };
        }
    });

export default CONTROLLER;