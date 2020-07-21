import angular from 'angular';

const CONTROLLER = 'addComment';

angular.module('webbit.controllers')
    .controller(CONTROLLER, function ($scope, Comment, SubComment, Alert) {
        this.$onInit = () => {
            const { threadId, parentCommentId = '' } = this;

            this.inputPlaceholder = parentCommentId ? 'Reply here...' : 'What do you think?';

            this.comment = {
                threadId,
                parentCommentId,
            }

            this.clearComment = () => {
                this.comment.content = '';
            }

            this.postComment = () => {
                (parentCommentId ? SubComment : Comment).post(this.comment)
                    .then(() => $scope.$emit('PostedComment'))
                    .then(() => this.clearComment())
                    .then(() => Alert.success('Your comment has been posted'))
            }
        };
    });

export default CONTROLLER;