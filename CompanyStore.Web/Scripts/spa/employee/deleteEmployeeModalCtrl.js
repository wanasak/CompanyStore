(function (app) {
    'use strict';

    app.controller('deleteEmployeeModalCtrl', deleteEmployeeModalCtrl);

    deleteEmployeeModalCtrl.$inject = ['$scope', '$modalInstance', 'employeeFullName'];

    function deleteEmployeeModalCtrl($scope, $modalInstance, employeeFullName) {

        $scope.fullName = employeeFullName;
        $scope.ok = ok;
        $scope.cancel = cancel;

        function ok() {
            $modalInstance.close();
        }

        function cancel() {
            $modalInstance.dismiss();
        }

    }


})(angular.module('companyStore'));