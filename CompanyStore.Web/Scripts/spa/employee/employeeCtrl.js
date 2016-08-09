(function (app) {
    'use strict';

    app.controller('employeeCtrl', employeeCtrl);

    employeeCtrl.$inject = ['$scope', 'DTOptionsBuilder', 'DTColumnBuilder'];

    function employeeCtrl($scope, DTOptionsBuilder, DTColumnBuilder) {

        // Datatables
        $scope.dtInstance = {};
        $scope.dtColumns = [
            DTColumnBuilder.newColumn('FirstName').withTitle('First'),
            DTColumnBuilder.newColumn('LastName').withTitle('Last'),
            DTColumnBuilder.newColumn('Email').withTitle('Email'),
            DTColumnBuilder.newColumn('IsActive').withTitle('Status'),
            DTColumnBuilder.newColumn(null).withTitle('Action').notSortable().renderWith(actionHtml)
        ]

        function loadEmployee() {
            $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
                dataSrc: "data",
                url: "/api/employeeCtrl?status=" + $scope.selectedStatus,
                type: "POST",
                header: { Authorization: $scope.$root.repository.loggedUser.authData }
            })
                .withOption('processing', true) // show progress bar
                .withOption('serverSide', true)
                .withOption('aaSorting', [0, 'asc'])
                .withOption('createRow', createRow)
                .withOption('responsive', true)
                .withPaginationType('full_numbers')
                .withDisplayLengthSize(5)
                .withDOM('ftrip')
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
        function createRow(row, data, dataIndex) {
            $complie(angular.element(row).contents())($scope);
        }
        function actionHtml(data, type, full, meta) {
            return '<button class="btn btn-warning" ng-click="edit(' + data.ID + ')">' +
               '   <i class="fa fa-edit"></i>' +
               '</button> | ' +
               '<button class="btn btn-danger" ng-click="delete(' + data.ID + ')" )"="">' +
               '   <i class="fa fa-trash-o"></i>' +
               '</button>';
        }

        loadEmployee();

    }

})(angular.module('companyStore'));