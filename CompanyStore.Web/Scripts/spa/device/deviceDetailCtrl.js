(function (app) {
    'use strict';

    app.controller('deviceDetailCtrl', deviceDetailCtrl);

    deviceDetailCtrl.$inject = ['$scope', 'apiService', '$routeParams', 'notificationService', '$modal'];

    function deviceDetailCtrl($scope, apiService, $routeParams, notificationService, $modal) {

        $scope.device = {};
        $scope.openRentModal = openRentModal;

        function openRentModal() {
            $modal.open({
                templateUrl: "scripts/spa/rental/rentalModal.html",
                controller: "rentalModalCtrl",
                scope: $scope,
                // resolve: {
                //     employeeId: function () {
                //         return employeeId;
                //     }
                // }
            }).result.then(function () {
                
            }, function (error) {
                // if (error) {
                //     notificationService.displayError(error);
                // }
            });
        }

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