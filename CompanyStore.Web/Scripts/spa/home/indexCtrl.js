(function (app) {
    'use strict';

    app.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', '$location', 'apiService', 'notificationService'];

    function indexCtrl($scope, $location, apiService, notificationService) {

        $scope.pageClass = 'page-home';
        $scope.loadingDevices = true;
        $scope.loadingCategories = true;

        $scope.latestDevices = [];

        apiService.get("/api/device/latest", null,
            deviceLoadCompleted,
            deviceLoadFailed);

        apiService.get("/api/category", null,
            categoryLoadCompleted,
            categoryLoadFailed);

        function deviceLoadCompleted(result) {
            $scope.latestDevices = result.data;
            $scope.loadingDevices = false;
        }

        function deviceLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function categoryLoadCompleted(result) {
            var categories = result.data;
            Morris.Bar({
                element: "categories-bar",
                data: categories,
                xkey: "Name",
                ykeys: ["NumberOfDevices"],
                labels: ["Number Of Devices"],
                barRatio: 0.4,
                xLabelAngle: 55,
                hideHover: "auto",
                resize: 'true'
            });
            $scope.loadingCategories = false;
        }

        function categoryLoadFailed(response) {
            notificationService.displayError(response.data);
        }
    }

})(angular.module('companyStore'));
