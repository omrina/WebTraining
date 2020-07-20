import angular from 'angular';

const CONTROLLER = 'addComment';

angular.module('webbit.controllers')
    .controller(CONTROLLER, function ($scope, Comment, Auth, Alert) {
        this.$onInit = () => {
            const { subwebbitId, threadId, parentCommentId = '' } = this;

            this.inputPlaceholder = parentCommentId ? 'Reply here...' : 'What do you think?';

            this.comment = {
                subwebbitId,
                threadId,
                parentCommentId,
                // TODO: content and author should be removed here (the server would find the author name)
                content: '',
                author: Auth.getCurrentUser().username
            }

            this.clearComment = () => {
                this.comment.content = '';
            }

            this.postComment = () => {
                Comment.post(this.comment)
                    .then(() => $scope.$emit('PostedComment'))
                    .then(() => this.clearComment())
                    .then(() => Alert.success('Your comment has been posted'))
            }
        };
    });

export default CONTROLLER;