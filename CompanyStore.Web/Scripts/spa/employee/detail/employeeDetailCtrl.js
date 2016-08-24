(function (app) {
    'use strict';

    app.controller('employeeDetailCtrl', employeeDetailCtrl);

    employeeDetailCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$routeParams'];

    function employeeDetailCtrl($scope, apiService, notificationService, $routeParams) {

       

    }

})(angular.module('companyStore'));