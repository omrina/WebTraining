import angular from 'angular';

angular.module('webbit.services')
    .service('Subwebbit', function ($http) {
        this.search = name =>
            $http.get(`/api/subwebbits/search/${name}`)
                .then(({data}) => data);
    });