import angular from 'angular';

angular.module('webbit.services')
    .service('UserStorage', function() {
        this.getUser = () => JSON.parse(localStorage.getItem('user'));
        
        this.setUser = user => {
            if (!user) {
                localStorage.removeItem('user');

                return;
            }

            localStorage.setItem('user', JSON.stringify(user));
        };
    });