import angular from 'angular';

angular.module('webbit.services').factory('Subwebbit', $resource => {
  return $resource(
    '/api/subwebbits/:id',
    {
      id: '@id',
    },
    {
      get: {
        method: 'GET',
      },
      create: {
        method: 'POST',
        url: '/api/subwebbits/:name',
        params: {
          name: '@name',
        },
      },
      delete: {
        method: 'DELETE',
      },
      search: {
        method: 'GET',
        isArray: true,
        url: '/api/subwebbits/search/:name',
      },
      subscribe: {
        method: 'POST',
        url: 'api/subwebbits/:id/subscribe',
      },
      unsubscribe: {
        method: 'POST',
        url: 'api/subwebbits/:id/unsubscribe',
      },
    }
  );
});