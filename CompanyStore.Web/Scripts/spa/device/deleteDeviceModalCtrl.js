(function (app) {
    'use strict';

    app.controller('deleteDeviceModalCtrl', deleteDeviceModalCtrl);

    deleteDeviceModalCtrl.$inject = ['$scope', '$modalInstance'];

    function deleteDeviceModalCtrl($scope, $modalInstance) {

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