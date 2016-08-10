(function (app) {
    'use strict';

    app.controller('deviceCtrl', deviceCtrl);

    deviceCtrl.$inject = ['$scope', 'apiService', 'notificationService'];

    function deviceCtrl($scope, apiService, notificationService) {

        $scope.loadingDevices = true;
        $scope.devices = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.search = search;
        $scope.clearSearch = clearSearch;

        function search(page) {
            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 6,
                    filter: $scope.filterDevices
                }
            };

            apiService.get("/api/device", config,
            deviceLoadCompleted,
            deviceLoadFailed);
        }

        function deviceLoadCompleted(result) {
            $scope.devices = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingDevices = false;
        }
        function deviceLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterDevices = '';
            search();
        }

        $scope.search();

    }

})(angular.module('companyStore'));