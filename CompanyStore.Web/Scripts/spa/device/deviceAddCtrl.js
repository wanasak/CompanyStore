(function (app) {
    'use strict';

    app.controller('deviceAddCtrl', deviceAddCtrl);

    deviceAddCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$location', 'FileUploader'];

    function deviceAddCtrl($scope, apiService, notificationService, $location, FileUploader) {

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

        // FileUpload
        //var uploader = $scope.uploader = new FileUploader();
        var uploader = $scope.uploader = new FileUploader({
            //url: 'api/device/add',
            queueLimit: 1
        });
        // FileUpload Add Authorization Header
        uploader.headers["Authorization"] = 'Basic ' + $scope.$root.repository.loggedUser.authData;
        // FileUpload Filter
        uploader.filters.push({
            name: 'imageFilter',
            fn: function (item /*{File|FileLikeObject}*/, options) {
                var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
                return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
            }
        });

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
            var deviceId = result.data;

            if (uploader.queue.length > 0) {
                uploader.onBeforeUploadItem = function (item) {
                    item.url = 'api/device/' + deviceId + '/upload/image';
                };
                uploader.uploadAll();
                uploader.onCompleteAll = function () {
                    redirectToDevicePage();
                };
            }
            else
                redirectToDevicePage();
            //if (deviceImage)
            //    fileUploadService.uploadImage(movieImage, $scope.movie.ID, redirectToDevicePage);
            //else
            //    redirectToDevicePage();
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