(function (app) {
    'use strict';

    app.controller('editEmployeeModalCtrl', editEmployeeModalCtrl);

    editEmployeeModalCtrl.$inject = ['$scope', '$modalInstance', 'employeeId', 'apiService', 'notificationService', 'FileUploader'];

    function editEmployeeModalCtrl($scope, $modalInstance, employeeId, apiService, notificationService, FileUploader) {

        $scope.ID = employeeId;
        //$scope.ok = ok;
        $scope.cancel = cancel;
        $scope.updateEmployee = updateEmployee;
        $scope.removeEmployeeImage = removeEmployeeImage;

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

        function removeEmployeeImage() {
            $scope.employee.Image = null;
        }

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
            if (uploader.queue.length > 0) {
                uploader.onBeforeUploadItem = function (item) {
                    item.url = 'api/employee/' + employeeId + '/upload/image';
                };
                uploader.uploadAll();
                uploader.onCompleteAll = function () {
                    $modalInstance.close();
                };
            }
            else
                $modalInstance.close();
        }
        function updateEmployeeFailed(response) {
            notificationService.displayError(response.data);
            $modalInstance.dismiss();
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