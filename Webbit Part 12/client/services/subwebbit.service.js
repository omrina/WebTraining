import angular from 'angular';

angular.module('webbit.services')
    .service('Subwebbit', function ($http) {
        this.search = name =>
            $http.get(`/api/subwebbits/search/${name}`)
                .then(({data}) => data);

        this.create = newSubwebbit =>
            $http.post('/api/subwebbits/create', newSubwebbit)
                .then(({data}) => data);

        this.get = id =>
            $http.get(`/api/subwebbits/${id}`)
                .then(({data}) => data);
    });