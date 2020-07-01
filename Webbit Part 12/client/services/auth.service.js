import angular from 'angular';

angular.module('webbit.services')
    .factory('Auth', $http => {
        let currentUser = {};

        const getUser = () => localStorage.getItem('user');
        const setUser = user => {
            if (!user) {
                localStorage.removeItem('user');

                return;
            }

            localStorage.setItem('user', JSON.stringify(user));
        };

        return {
            signup: ({username = '', password = ''} = {}) => $http.post('/auth/signup', {username, password}),
            login ({username = '', password = ''} = {}) {
                return $http.post('/auth/login', {username, password})
                    .then(({data}) => {
                        setUser(data);
                        currentUser = data;
                    })
                    .catch(({status} = {}) => {
                        setUser(null);

                        return Promise.reject(status);
                    });
            },
            logout() {
              setUser(null);
            },
            getCurrentUser: () => JSON.parse(getUser()),
            isLoggedIn: () => !!getUser(),
            getUser: getUser,
            setUser: setUser
        };
    });
