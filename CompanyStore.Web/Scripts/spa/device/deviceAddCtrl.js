(function (app) {
    'use strict';

    app.controller('deviceAddCtrl', deviceAddCtrl);

    deviceAddCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$location'];

    function deviceAddCtrl($scope, apiService, notificationService, $location) {

        var deviceImage = null;
        $scope.prepareFile = prepareFile;

        $scope.device = { NumberOfStocks: 1, CreatedDate: new Date() };
        $scope.categories = [];
        $scope.addDevice = addDevice;
        $scope.datepicker = {};
        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1,
            showWeeks: false
        };

        function prepareFile($files) {
            deviceImage = $files;
        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.datepicker.opened = true;
        }

        function addDevice() {
            //console.log($scope.device);
            apiService.post("api/device/add", $scope.device,
            addDeviceCompleted,
            addDeviceFailed);
        }

        function redirectToDevicePage() {
            $location.url('device');
        }
        
        function addDeviceCompleted(result) {
            var deviceId = result.data.ID;
            if (deviceImage)
                fileUploadService.uploadImage(movieImage, $scope.movie.ID, redirectToDevicePage);
            else
                redirectToDevicePage();
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