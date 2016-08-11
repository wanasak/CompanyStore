(function (app) {
    'use strict';

    app.controller('deviceDetailCtrl', deviceDetailCtrl);

    deviceDetailCtrl.$inject = ['$scope', 'apiService', '$routeParams', 'notificationService', '$modal'];

    function deviceDetailCtrl($scope, apiService, $routeParams, notificationService, $modal) {

        $scope.device = {};
        $scope.openRentModal = openRentModal;
        $scope.rentalHistory = [];
        $scope.returnDevice = returnDevice;
        $scope.getStatusColor = getStatusColor;
        $scope.isBorrowed = isBorrowed;

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
                loadPage();
            }, function (error) {
                // if (error) {
                //     notificationService.displayError(error);
                // }
            });
        }
        // Load Device Detail
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
        // Load Rentals
        function loadRentalHistory() {
            apiService.get("api/rental/" + $routeParams.id + "/rentalHistory",
                null,
                loadRentalHistoryCompleted,
                loadRentalHistoryFailed);
        }
        function loadRentalHistoryCompleted(result) {
            $scope.rentalHistory = result.data;
        }
        function loadRentalHistoryFailed(response) {
            notificationService.displayError(response.data);
        }
        // Return Device Action
        function returnDevice(rentalID) {
            apiService.post("api/rental/return/" + rentalID,
                null,
                returnDeviceCompleted,
                returnDeviceFailed);
        }
        function returnDeviceCompleted(result) {
            notificationService.displaySuccess('Movie returned to HomeCinema succeesfully');
            loadPage();
        }
        function returnDeviceFailed(response) {
            notificationService.displayError(response.data);
        }

        function getStatusColor(status) {
            if (status == 'Borrowed')
                return 'red'
            else {
                return 'green';
            }
        }

        function isBorrowed(rental) {
            return rental.Status == 'Borrowed';
        }

        function loadPage() {
            loadDeviceDetail();
            loadRentalHistory();
        }

        loadPage();

    }

})(angular.module('companyStore'));