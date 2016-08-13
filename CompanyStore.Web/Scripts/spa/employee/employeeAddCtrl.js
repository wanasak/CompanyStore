(function (app) {
    'use strict';

    app.controller('employeeAddCtrl', employeeAddCtrl);

    employeeAddCtrl.$inject = ['$scope', 'apiService', '$location', 'FileUploader'];

    function employeeAddCtrl($scope, apiService, $location, FileUploader) {

        $scope.employee = { CreatedDate: new Date() };
        $scope.registerEmployee = registerEmployee;
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
            var employeeId = result.data;
            if (uploader.queue.length > 0) {
                uploader.onBeforeUploadItem = function (item) {
                    item.url = 'api/employee/' + employeeId + '/upload/image';
                };
                uploader.uploadAll();
                uploader.onCompleteAll = function () {
                    $location.url('employee');
                };
            }
            else
                $location.url('employee');
            
        }
        function registerEmployeeFailed(response) {
            
        }

    }

})(angular.module('companyStore'));