(function (app) {
    'use strict';

    app.controller('deviceDetailCtrl', deviceDetailCtrl);

    deviceDetailCtrl.$inject = ['$scope', 'apiService', '$routeParams', 'notificationService'];

    function deviceDetailCtrl($scope, apiService, $routeParams, notificationService) {

        $scope.device = {};

        function loadDeviceDetail() {
            apiService.get("api/device/" + $routeParams.id,
                null,
                loadDeviceDetailCompleted,
                loadDeviceDetailFailed);
        }

        function loadDeviceDetailCompleted(result) {
            $scope.device = result.data;
        }

        function loadDeviceDetailFailed(response) {
            //notificationService.displayError(response.);
        }

        loadDeviceDetail();

    }

})(angular.module('companyStore'));