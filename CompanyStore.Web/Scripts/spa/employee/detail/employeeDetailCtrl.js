(function (app) {
    'use strict';

    app.controller('employeeDetailCtrl', employeeDetailCtrl);

    employeeDetailCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$routeParams'];

    function employeeDetailCtrl($scope, apiService, notificationService, $routeParams) {

        $scope.employee = {};
        $scope.rentals = [];
        $scope.returnDevice = returnDevice;
        $scope.getStatusColor = getStatusColor;
        $scope.clearSearch = clearSearch;
        $scope.isBorrowed = isBorrowed;

        // Load Employee
        function loadEmployee() {
            apiService.get("api/employee/" + $routeParams.id, null,
                loadEmployeeCompleted,
                loadEmployeeFailed);
        }
        function loadEmployeeCompleted(result) { $scope.employee = result.data; }
        function loadEmployeeFailed(response) { notificationService.displayError(response.data); }
        // Load Rentals
        function loadEmployeeRentals() {
            apiService.get("api/rental/employee/" + $routeParams.id, null,
                loadEmployeeRentalsCompleted,
                loadEmployeeRentalsFailed);
        }
        function loadEmployeeRentalsCompleted(result) { $scope.rentals = result.data; }
        function loadEmployeeRentalsFailed(response) { notificationService.displayError(response.data); }
        // Return Rental
        function returnDevice(rentalID) {
            apiService.post("api/rental/return/" + rentalID,
                null,
                returnDeviceCompleted,
                returnDeviceFailed);
        }
        function returnDeviceCompleted(result) {
            notificationService.displaySuccess('Device returned to store succeesfully');
            loadEmployeeRentals();
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
        function clearSearch() {
            $scope.filterRentals = "";
        }

        loadEmployee();
        loadEmployeeRentals();
    }

})(angular.module('companyStore'));