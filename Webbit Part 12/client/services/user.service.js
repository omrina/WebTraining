import angular from 'angular';

angular.module('webbit.services')
    .factory('User', $resource => {
        return $resource('/api/users/:id', {
            id: '@_id'
        }, {
            me: {
                method: 'GET',
                params: {
                    id: 'me'
                }
            }
        });
    });