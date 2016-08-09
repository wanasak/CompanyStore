(function (app) {
    'use strict';

    app.controller('employeeCtrl', employeeCtrl);

    employeeCtrl.$inject = ['$scope', 'DTOptionsBuilder', 'DTColumnBuilder', 'apiService', '$compile', 'notificationService'];

    function employeeCtrl($scope, DTOptionsBuilder, DTColumnBuilder, apiService, $complie, notificationService) {

        // Datatables
        var defaultPageSize = 10;
        $scope.dtInstance = {};
        $scope.dtColumns = [
            DTColumnBuilder.newColumn('FirstName').withTitle('First'),
            DTColumnBuilder.newColumn('LastName').withTitle('Last'),
            DTColumnBuilder.newColumn('Email').withTitle('Email'),
            //DTColumnBuilder.newColumn('IsActive').withTitle('Status'),
            DTColumnBuilder.newColumn('IsActive').withTitle('Status').notSortable().renderWith(statusHtml),
            DTColumnBuilder.newColumn(null).withTitle('Action').notSortable().renderWith(actionHtml)
        ];

        function loadEmployee() {
            $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
                dataSrc: "data",
                url: "/api/employee?status=" + "all", //$scope.selectedStatus,
                type: "POST",
                headers: { Authorization: $scope.$root.repository.loggedUser.authData }
            })
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(defaultPageSize) // Page size
            .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column
            .withDOM('frtip')
            .withOption('createdRow', createdRow)
            .withOption('responsive', true)
            .withButtons([
                {
                    text: 'Reload',
                    key: '1',
                    action: function (e, dt, node, config) {
                        $scope.dtInstance.reloadData(); // reload datatables
                    }
                },
                'print'
            ]);
        }
        function createdRow(row, data, dataIndex) {
            $complie(angular.element(row).contents())($scope);
        }
        function statusHtml(data, type, full, meta) {
            if (data === true ) {
                return '<span class="label label-success">Active</span>';
            }
            else {
                return '<span class="label label-default">Inactive</span>';
            }
        }
        function actionHtml(data, type, full, meta) {
            return '<button class="btn btn-warning" ng-click="edit(' + data.ID + ')">' +
               '   <i class="fa fa-edit"></i>' +
               '</button> &nbsp; ' +
               '<button class="btn btn-danger" ng-click="delete(' + data.ID + ')" )"="">' +
               '   <i class="fa fa-trash-o"></i>' +
               '</button>';
        }

        loadEmployee();

    }

})(angular.module('companyStore'));