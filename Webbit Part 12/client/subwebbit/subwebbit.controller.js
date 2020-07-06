import angular from 'angular';
import createThreadTemplate from './create-thread/create-thread.html';
import createThreadController from './create-thread/create-thread.controller';
import './create-thread/create-thread.less';
import TimeAgo from 'time-ago';

const CONTROLLER = 'subwebbit';

angular.module('webbit.controllers')
    .controller(CONTROLLER, ($scope, $state,$timeout, $mdDialog, Subwebbit) => {
        const id = $state.params.id;
        // TODO: get from server if subscribed (AND if owner)!
        $scope.isSubscribed = true;
        $scope.toTimeAgo = date => TimeAgo.ago(date);

        $scope.showCreateDialog = () => {
            $mdDialog.show({
                locals: {targetSubwebbit: $scope.subwebbit},
                controller: createThreadController,
                template: createThreadTemplate,
            }).then(() => {
                $timeout(() => $scope.threadsFetcher.reloadThreads(), 1000);
            });
        };

        Subwebbit.get(id)
            .then(subwebbit => {
                $scope.subwebbit = subwebbit;
                $scope.threadsFetcher = {
                    subwebbitId: $scope.subwebbit.id,
                    threads: [],
                    hasFetchedAllThreads: false,
                    isRequestInProgress: false,

                    reloadThreads() {
                        this.threads = [],
                        this.hasFetchedAllThreads = false;
                    },
          
                    getItemAtIndex(index) {
                        if (this.threads[index]) {
                            return this.threads[index];
                        }
                        else if (!this.isRequestInProgress && !this.hasFetchedAllThreads) {
                            this.isRequestInProgress = true;
                            this.fetchMoreItems_(index);
                        }
            
                        return null;
                    },
          
                    getLength() {
                        return this.threads.length + 1;
                    },
          
                    fetchMoreItems_(index) {
                        const ids = this.subwebbitId;
                        Subwebbit.getThreads({
                            id: ids,
                            index: index,
                            amount: 3,
                        })
                        .then(angular.bind(this, function (threads) {
                            this.hasFetchedAllThreads = !threads.length;
                            this.threads = this.threads.concat(threads);
                            this.isRequestInProgress = false;
                        }));
                    }
                  };
            });
    });

export default CONTROLLER;