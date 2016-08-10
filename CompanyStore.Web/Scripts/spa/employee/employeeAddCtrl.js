(function (app) {
    'use strict';

    app.controller('employeeAddCtrl', employeeAddCtrl);

    employeeAddCtrl.$inject = ['$scope', 'apiService', '$location'];

    function employeeAddCtrl($scope, apiService, $location) {

        $scope.employee = { CreatedDate: new Date() };
        $scope.registerEmployee = registerEmployee;
        $scope.datepicker = {};
        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1,
            showWeeks: false
        };

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.datepicker.opened = true;
        }

        function registerEmployee() {
            apiService.post("api/employee/register", $scope.employee,
            registerEmployeeCompleted,
            registerEmployeeFailed);
        }
        function registerEmployeeCompleted(result) {
            $location.url('employee');
        }
        function registerEmployeeFailed(response) {
            
        }

    }

})(angular.module('companyStore'));