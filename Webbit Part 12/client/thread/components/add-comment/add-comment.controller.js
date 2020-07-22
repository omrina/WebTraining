import angular from 'angular';

const CONTROLLER = 'addComment';

angular.module('webbit.controllers')
    .controller(CONTROLLER, function ($scope, Comment, SubComment, Alert) {
        this.inputPlaceholder = () => this.parentCommentId ? 'Reply here...' : 'What do you think?';

        this.$onInit = () => {
            this.comment = {
                threadId: this.threadId,
                parentCommentId: this.parentCommentId,
            }
        }

        this.clearComment = () => {
            this.comment.content = '';
        }

        this.postComment = () => {
            (this.parentCommentId ? SubComment : Comment).post(this.comment)
                .then(() => $scope.$emit('PostedComment'))
                .then(() => this.clearComment())
                .then(() => Alert.success('Your comment has been posted'))
        }
    });

export default CONTROLLER;