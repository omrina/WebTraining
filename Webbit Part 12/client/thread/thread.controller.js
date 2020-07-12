import angular from "angular";

const CONTROLLER = "thread";

angular.module("webbit.controllers")
  .controller(CONTROLLER, ($scope, $stateParams, Thread, Comment) => {
    const { subwebbitId, threadId } = $stateParams;
    const reloadComments = () =>
      Comment.getAll(subwebbitId, threadId)
        .then(comments => $scope.comments = comments);

    Thread.get(subwebbitId, threadId)
      .then(thread => { $scope.thread = thread; });

    $scope.$on('PostedComment', () => {
      reloadComments()
        .then(() => {$scope.thread.commentsCount++;});
    })

    reloadComments();
  });

export default CONTROLLER;