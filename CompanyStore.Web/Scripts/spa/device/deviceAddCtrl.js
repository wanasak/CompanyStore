(function (app) {
    'use strict';

    app.controller('deviceAddCtrl', deviceAddCtrl);

    deviceAddCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$location'];

    function deviceAddCtrl($scope, apiService, notificationService, $location) {

        $scope.pageClass = 'container';
        $scope.device = {};
        $scope.categories = [];
        $scope.addDevice = addDevice;

        function addDevice() {
            //console.log($scope.device);
            apiService.post("api/device/add", $scope.device,
            addDeviceCompleted,
            addDeviceFailed);
        }
        
        function addDeviceCompleted(result) {
            $location.path("#/device");
        }

        function addDeviceFailed(response) {

        }

        function loadCategories() {
            apiService.get("api/category", null,
            loadCategoriesCompleted,
            loadCategoriesFailed);
        }
        
        function loadCategoriesCompleted(result) {
            $scope.categories = result.data;
        }

        function loadCategoriesFailed(response) {

        }

        loadCategories();

    }

})(angular.module('companyStore'));