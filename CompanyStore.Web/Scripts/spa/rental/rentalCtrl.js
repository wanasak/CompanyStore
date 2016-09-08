(function (app) {
    'use strict';

    app.controller('rentalCtrl', rentalCtrl);

    rentalCtrl.$inject = ['$scope', 'apiService', 'notificationService'];

    function rentalCtrl($scope, apiService, notificationService) {

        $scope.rentalHistory = [];
        $scope.getStatusColor = getStatusColor;

        // Load Rentals
        function loadRentalHistory() {
            apiService.get("api/rental",
                null,
                loadRentalHistoryCompleted,
                loadRentalHistoryFailed);
        }
        function loadRentalHistoryCompleted(result) {
            $scope.rentalHistory = result.data;
        }
        function loadRentalHistoryFailed(response) {
            notificationService.displayError(response.data);
        }
        function getStatusColor(status) {
            if (status == 'Borrowed')
                return 'red'
            else {
                return 'green';
            }
        }

        loadRentalHistory();

    }

})(angular.module('companyStore'));