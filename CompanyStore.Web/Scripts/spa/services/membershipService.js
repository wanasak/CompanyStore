(function (app) {
    'use strict';

    app.factory('membershipService', membershipService);

    membershipService.$inject = ['apiService', 'notificationService', '$http', '$base64', '$cookieStore', '$rootScope'];

    function membershipService(apiService, notificationService, $http, $base64, $cookieStore, $rootScope) {

        var service = {
            login: login,
            register: register,
            saveCredentials: saveCredentials,
            removeCredentials: removeCredentials,
            isUserLoggedIn: isUserLoggedIn
        };

        function login(user, completed) {
            apiService.post("api/account/authenticate", user,
                completed,
                loginFailed);
        }

        function register(user, completed) {
            apiService.post("api/account/register", user,
                completed,
                registerFailed);
        }

        function loginFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function registerFailed(response) {
            notificationService.displayError('Registration failed. Try again.');
        }

        function saveCredentials(user) {
            var membershipData = $base64.encode(user.username + ':' + user.password);
            $rootScope.repository = {
                loggedUser: {
                    username: user.username,
                    authData: membershipData
                }
            };
            $http.defaults.headers.common['Authorization'] = 'Basic ' + membershipData;
            $cookieStore.put('repository', $rootScope.repository);
        }

        function removeCredentials() {
            $rootScope.repository = {};
            $cookieStore.remove('repository');
            //$http.defaults.headers.common.Authorization = '';
            $http.defaults.headers.common['Authorization'] = '';
        }

        function isUserLoggedIn() {
            return $rootScope.repository.loggedUser != null;
        }

        return service;
    }

})(angular.module('common.core'));