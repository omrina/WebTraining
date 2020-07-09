import angular from 'angular';

const CONTROLLER = 'rating';

angular.module('webbit.controllers')
    .controller(CONTROLLER, function(Rating) {
        // $scope.$on('CreateNewThread', () => {
        //     this.threadsFetcher.reloadThreads();
        // });
        
        
        this.$onInit = () => {
            this.upvote = () => Rating.upvote(this.itemId);
            this.downvote = () => Rating.downvote(this.itemId);
        }
    });

export default CONTROLLER;