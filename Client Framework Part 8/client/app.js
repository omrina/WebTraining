const app = angular.module('mailApp', ['ngResource', 'ngMaterial', 'ui.router']);

app.config($stateProvider => {
    const incomingState = {
        name: 'incoming',
        url: '/incoming',
        component: 'messagesComponent',
        resolve: {
            messages: $http => {
                return $http.get('http://localhost:3000/incoming/')
                    .then(({ data }) => data,
                        () => console.log('getting incoming messages was failure'));
            },
            addressPrefix: () => 'From',
            addressProp: () => 'from'
        }
    }

    const outgoingState = {
        name: 'outgoing',
        url: '/outgoing',
        component: 'messagesComponent',
        resolve: {
            messages: $http => {
                return $http.get('http://localhost:3000/outgoing/')
                    .then(({ data }) => data,
                        () => console.log('getting outgoing messages was failure'));
            },
            addressPrefix: () => 'To',
            addressProp: () => 'to'
        }
    }

    $stateProvider.state(incomingState);
    $stateProvider.state(outgoingState);
});

app.component('messagesComponent', {
    // templateUrl: './messages-template.html',
    bindings: {
        messages: '<',
        addressPrefix: '@',
        addressProp: '@'
    },
    template: '<div ng-repeat="msg in $ctrl.messages" class="message-box">' +
        '<div class="message-header">' +
        '<div class= "message-subject" >' +
        '<span>{{ msg.subject }}</span>' +
        '</div>' +
        '<span>' +
        '<b>{{ $ctrl.addressPrefix }}: </b>{{ msg[$ctrl.addressProp] }}' +
        '</span>' +
        '</div >' +
        '<div class="vertical-separator"></div>' +
        '<span class="message-body">{{ msg.content }}</span>' +
        '<i class="mdi mdi-alert importance-icon"></i></div>',
});

