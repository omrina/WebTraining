import angular from 'angular';

const CONTROLLER = 'threadsDisplayer';

angular.module('webbit.controllers')
    .controller(CONTROLLER, function($scope, Subwebbit) {
        $scope.$on('CreateNewThread', () => {
            this.threadsFetcher.reloadThreads();
        });
        
        this.$onInit = () => {
            const subwebbitId = this.subwebbitId;

            this.threadsFetcher = {
                subwebbitId,
                threads: [],
                hasFetchedAllThreads: false,
                isRequestInProgress: false,

                reloadThreads() {
                    (this.threads = []), (this.hasFetchedAllThreads = false);
                },

                getItemAtIndex(index) {
                    if (this.threads[index]) {
                        return this.threads[index];
                    }

                    if (!this.isRequestInProgress && !this.hasFetchedAllThreads) {
                        this.isRequestInProgress = true;
                        this.fetchMoreItems_(index);
                    }

                    return null;
                },

                getLength() {
                    return this.threads.length + 1;
                },

                fetchMoreItems_(index) {
                    Subwebbit.getThreads({
                        id: this.subwebbitId,
                        index
                    })
                    .then(angular.bind(this, function (threads) {
                        this.hasFetchedAllThreads = !threads.length;
                        this.threads = this.threads.concat(threads);
                        this.isRequestInProgress = false;
                    })
                    );
                },
            };
        }
    });

export default CONTROLLER;