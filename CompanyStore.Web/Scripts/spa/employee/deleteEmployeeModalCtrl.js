(function (app) {
    'use strict';

    app.controller('deleteEmployeeModalCtrl', deleteEmployeeModalCtrl);

    deleteEmployeeModalCtrl.$inject = ['$scope', '$modalInstance', 'employeeId'];

    function deleteEmployeeModalCtrl($scope, $modalInstance, employeeId) {

        $scope.id = employeeId;
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