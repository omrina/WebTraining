import angular from 'angular';

const CONTROLLER = 'addComment';

angular.module('webbit.controllers')
    .controller(CONTROLLER, function ($scope, Comment, Auth) {
        this.$onInit = () => {
            const { subwebbitId, threadId, parentCommentId = '' } = this;

            this.inputPlaceholder = parentCommentId ? 'Reply here...' : 'What do you think?';

            this.comment = {
                subwebbitId,
                threadId,
                parentCommentId,
                content: '',
                author: Auth.getCurrentUser().username
            }

            this.clearComment = () => {
                this.comment.content = '';
            }

            this.postComment = () => {
                Comment.post(this.comment).then(() => {
                    $scope.$emit('PostedComment');
                    this.clearComment();
                })
            }
        };
    });

export default CONTROLLER;