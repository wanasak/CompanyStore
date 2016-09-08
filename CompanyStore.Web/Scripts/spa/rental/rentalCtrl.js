(function (app) {
    'use strict';

    app.controller('rentalCtrl', rentalCtrl);

    rentalCtrl.$inject = ['$scope', 'apiService', 'notificationService', 'uiCalendarConfig'];

    function rentalCtrl($scope, apiService, notificationService, uiCalendarConfig) {

        $scope.rentalHistory = [];
        $scope.getStatusColor = getStatusColor;
        // Config Calendar
        $scope.SelectedEvent = null;
        var isFirstTime = true;
        $scope.events = [];
        $scope.eventSources = [$scope.events];
        $scope.uiConfig = {
            calendar: {
                height: 450,
                //editable: true,
                displayEventTime: false,
                header: {
                    left: '', //month basicWeek basicDay agendaWeek agendaDay',
                    center: 'title',
                    right:'today prev,next'
                },
                eventClick: function (event) {
                    $scope.SelectedEvent = event;
                },
                timezone: 'UTC',
                //eventStartEditable: false,
                eventAfterAllRender: function () {
                    if ($scope.events.length > 0 && isFirstTime) {
                        //Focus first event
                        uiCalendarConfig.calendars.myCalendar.fullCalendar('gotoDate', $scope.events[0].start);
                        isFirstTime = false;
                    }
                }
            }
        }
        function setupCalendar(data) {
            angular.forEach(data, function (value) {
                $scope.events.push({
                    id: value.ID,
                    title: value.Employee,
                    description: value.Device,
                    start: new Date(value.RentalDate),
                    end: new Date(value.ReturnedDate),
                    allDay : true,
                    stick: true,
                    color: value.ReturnedDate == null ? 'red' : 'green'
                });
            });
        }
        // Load Rentals
        function loadRentalHistory() {
            apiService.get("api/rental",
                null,
                loadRentalHistoryCompleted,
                loadRentalHistoryFailed);
        }
        function loadRentalHistoryCompleted(result) {
            $scope.rentalHistory = result.data;
            setupCalendar(result.data);
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