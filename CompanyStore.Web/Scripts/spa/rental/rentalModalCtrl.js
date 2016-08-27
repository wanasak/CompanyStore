(function (app) {
    'use strict';

    app.controller('rentalModalCtrl', rentalModalCtrl);

    rentalModalCtrl.$inject = ['$scope', 'apiService', '$modalInstance', 'notificationService'];

    function rentalModalCtrl($scope, apiService, $modalInstance, notificationService) {

        $scope.rent = rent;
        $scope.cancel = cancel;
        $scope.stockDevices = [];
        $scope.selectEmployee = selectEmployee;
        $scope.selectionChanged = selectionChanged;
        $scope.selectedEmployee = -1;
        $scope.isEnabled = false;

        function loadStockDevices() {
            apiService.get("api/stock/device/" + $scope.device.ID, null,
            loadStockDevicesCompleted,
            loadStockDevicesFailed)
        }
        function loadStockDevicesCompleted(result) {
            $scope.stockDevices = result.data;
            $scope.selectedStockDevice = $scope.stockDevices[0].ID;
        }
        function loadStockDevicesFailed(response) {
            notificationService.displayError(response.data.Message);
        }

        function rent() {
            apiService.post("api/rental/rent/" + $scope.selectedEmployee + "/" + $scope.selectedStockDevice, null,
            rentCompleted,
            rentFailed);
        }
        function rentCompleted(result) {
            $modalInstance.close();
        }
        function rentFailed(response) {
            //notificationService.displayError(response.data);
            $scope.errorMsg = response.data;
        }

        function selectEmployee($item) {
            if ($item) {
                $scope.selectedEmployee = $item.originalObject.ID;
                $scope.isEnabled = true;
            }
            else {
                $scope.selectedEmployee = -1;
                $scope.isEnabled = false;
            }
        }

        function selectionChanged($item) {}

        function cancel() {
            $scope.stockDevices = [];
            $scope.selectedEmployee = -1;
            $modalInstance.dismiss();
        }

        loadStockDevices();

    }

})(angular.module('companyStore'));