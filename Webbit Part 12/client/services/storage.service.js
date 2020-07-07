import angular from 'angular';

angular.module('webbit.services')
    .service('Storage', function() {
        this.getUser = () => JSON.parse(localStorage.getItem('user'));
        
        this.setUser = user => {
            if (!user) {
                localStorage.removeItem('user');

                return;
            }

            localStorage.setItem('user', JSON.stringify(user));
        };
    });