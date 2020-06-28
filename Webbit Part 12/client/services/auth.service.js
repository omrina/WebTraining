import angular from 'angular';

angular.module('webbit.services')
    .factory('Auth', ($http) => {
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
            login ({username = '', password = ''} = {}) {
                return $http.post('/auth/login', {username, password})
                    .then(({data}) => {
                        setUser(data);
                        currentUser = data;

                        return Promise.resolve();
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
            isLoggedIn () {
                return !!getUser();
            },
            getUser: getUser,
            setUser: setUser
        };
    });
