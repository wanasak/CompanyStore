(function (app) {
    'use strict';

    app.controller('deviceDetailCtrl', deviceDetailCtrl);

    deviceDetailCtrl.$inject = ['$scope', 'apiService', '$routeParams', 'notificationService', '$modal', '$location'];

    function deviceDetailCtrl($scope, apiService, $routeParams, notificationService, $modal, $location) {

        $scope.device = {};
        $scope.openRentModal = openRentModal;
        $scope.openDeleteModal = openDeleteModal;
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
        function openDeleteModal(id) {
            $modal.open({
                templateUrl: "scripts/spa/device/deleteDeviceModal.html",
                controller: "deleteDeviceModalCtrl",
                scope: $scope
            }).result.then(function () {
                apiService.delete("api/device/" + id, null,
                deleteDeviceCompleted,
                deleteDeviceFailed);
            }, function (error) {
            });
        }
        function deleteDeviceCompleted(result) {
            $location.path("#/device");
        }
        function deleteDeviceFailed(response) {
            notificationService.displayError(response.data);
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
        // Return Rental
        function returnDevice(rentalID) {
            apiService.post("api/rental/return/" + rentalID,
                null,
                returnDeviceCompleted,
                returnDeviceFailed);
        }
        function returnDeviceCompleted(result) {
            notificationService.displaySuccess('Device returned to store succeesfully');
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