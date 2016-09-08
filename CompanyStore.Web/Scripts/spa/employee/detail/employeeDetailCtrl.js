(function (app) {
    'use strict';

    app.controller('employeeDetailCtrl', employeeDetailCtrl);

    employeeDetailCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$routeParams', '$timeout'];

    function employeeDetailCtrl($scope, apiService, notificationService, $routeParams, $timeout) {

        // Morris Chart
        var rentalHistoryChart = null;
        var totalRentalByDate = null;

        $scope.employee = {};
        $scope.rentals = [];
        $scope.returnDevice = returnDevice;
        $scope.getStatusColor = getStatusColor;
        $scope.clearSearch = clearSearch;
        $scope.isBorrowed = isBorrowed;
        $scope.loadEmployeeRentals = loadEmployeeRentals;
        $scope.disableRentalColumn = disableRentalColumn;
        //$scope.filterStatus = "All";

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
        function loadEmployeeRentalsCompleted(result) {
            $scope.rentals = result.data.RentalHistories;
            totalRentalByDate = result.data.TotalRentalsByDate;
            
        }
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
        function disableRentalColumn() {
            return $scope.filterStatus == "Returned";
        }
        // Morrisjs display collapse in bootstrap tab
        $scope.redrawChart = function () {
            $timeout(function () {
                //loadEmployeeRentals();
                var data = totalRentalByDate //result.data.TotalRentalsByDate;
                if (!rentalHistoryChart) {
                    rentalHistoryChart = Morris.Line({
                        element: 'rentalHistoryByDateChart',
                        data: data.length ? data : [{ Date: "No Rental Data", TotalRentals: 0 }],
                        xkey: 'Date',
                        xLabels: 'day',
                        ykeys: ['TotalRentals'],
                        labels: ['Total'],
                        resize: true
                    });
                } else {
                    rentalHistoryChart.setData(data);
                }
            }, 200); // delay 200 ms
        }

        loadEmployee();
        loadEmployeeRentals();
    }

})(angular.module('companyStore'));