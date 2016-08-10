(function (app) {
    'use strict';

    app.controller('editEmployeeModalCtrl', editEmployeeModalCtrl);

    editEmployeeModalCtrl.$inject = ['$scope', '$modalInstance', 'employeeId', 'apiService'];

    function editEmployeeModalCtrl($scope, $modalInstance, employeeId, apiService) {

        $scope.ID = employeeId;
        //$scope.ok = ok;
        $scope.cancel = cancel;
        $scope.updateEmployee = updateEmployee;

        function loadEmployee() {
            apiService.get("api/employee/" + employeeId, null,
            loadEmployeeCompleted,
            loadEmployeeFailed);
        }
        function loadEmployeeCompleted(result) {
            $scope.employee = result.data;
        }
        function loadEmployeeFailed(response) {
            $scope.errorMsg = response.data;
        }

        function updateEmployee() {
            apiService.post("api/employee/update/" + employeeId, $scope.employee,
            updateEmployeeCompleted,
            updateEmployeeFailed);
        }
        function updateEmployeeCompleted(result) {
            $modalInstance.close();
        }
        function updateEmployeeFailed(response) {
            $modalInstance.dismiss(response.data);
        }

        // function ok() {
        //     $modalInstance.close();
        // }

        function cancel() {
            $modalInstance.dismiss();
        }

        loadEmployee();

    }


})(angular.module('companyStore'));