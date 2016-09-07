(function (app) {
    'use strict';

    app.controller('employeeCtrl', employeeCtrl);

    employeeCtrl.$inject = ['$scope', 'DTOptionsBuilder', 'DTColumnBuilder', 'apiService', '$compile', 'notificationService', '$modal'];

    function employeeCtrl($scope, DTOptionsBuilder, DTColumnBuilder, apiService, $complie, notificationService, $modal) {

        // Datatables
        var defaultPageSize = 10;
        $scope.dtInstance = {};
        $scope.dtColumns = [
            DTColumnBuilder.newColumn('ID').withTitle('ID'),
            DTColumnBuilder.newColumn('FirstName').withTitle('First'),
            DTColumnBuilder.newColumn('LastName').withTitle('Last'),
            DTColumnBuilder.newColumn('Email').withTitle('Email'),
            DTColumnBuilder.newColumn('DepartmentName').withTitle('Department'),
            //DTColumnBuilder.newColumn('IsActive').withTitle('Status'),
            DTColumnBuilder.newColumn('IsActive').withTitle('Status').notSortable().renderWith(statusHtml),
            DTColumnBuilder.newColumn(null).withTitle('Action').notSortable().renderWith(actionHtml)
        ];
        $scope.deleteEmployee = deleteEmployee;
        $scope.editEmployee = editEmployee;

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
                    'colvis',
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: ':visible'
                        },
                        // Remove last column
                        customize: function (win) {
                            $(win.document.body).find('table').find('td:last-child, th:last-child').remove();
                        }
                    }
                ]);
        }
        function createdRow(row, data, dataIndex) {
            $complie(angular.element(row).contents())($scope);
        }
        function statusHtml(data, type, full, meta) {
            if (data === true) {
                return '<span class="label label-success">Active</span>';
            }
            else {
                return '<span class="label label-default">Inactive</span>';
            }
        }
        function actionHtml(data, type, full, meta) {
            return '<a class="btn btn-info" title="Detail" data-toggle="popover" data-trigger="hover" href="#/employee/' + data.ID + '">' +
                '   <span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>' +
                '</a> &nbsp; ' +
                '<button class="btn btn-warning" title="Edit" data-toggle="popover" data-trigger="hover" ng-click="editEmployee(' + data.ID + ')">' +
                '   <i class="fa fa-edit"></i>' +
                '</button> &nbsp; ' +
                '<button class="btn btn-danger" title="Delete" data-toggle="popover" data-trigger="hover" ng-click="deleteEmployee(' + data.ID + ',\'' + data.FullName + '\')">' +
                '   <i class="fa fa-trash-o"></i>' +
                '</button>';
        }
        function editEmployee(employeeId) {
            $modal.open({
                //backdrop: 'static',
                templateUrl: "scripts/spa/employee/editEmployeeModal.html",
                controller: "editEmployeeModalCtrl",
                scope: $scope,
                resolve: {
                    employeeId: function () {
                        return employeeId;
                    }
                }
            }).result.then(function () {
                notificationService.displaySuccess("Update employee completed");
                // Then reload the data so that DT is refreshed
                $scope.dtInstance.reloadData(null, false);
            }, function () { });
        }
        function deleteEmployee(employeeId, employeeFullName) {
            $modal.open({
                templateUrl: "scripts/spa/employee/deleteEmployeeModal.html",
                controller: "deleteEmployeeModalCtrl",
                size: "sm",
                scope: $scope,
                resolve: {
                    employeeFullName: function () {
                        return employeeFullName;
                    }
                }
            }).result.then(function () {
                apiService.delete("api/employee/delete/" + employeeId, null,
                    deleteEmployeeCompleted,
                    deleteEmployeeFailed);
            }, function () { });
        }
        function deleteEmployeeCompleted(result) {
            // Then reload the data so that DT is refreshed
            $scope.dtInstance.reloadData();
        }
        function deleteEmployeeFailed(response) {
            notificationService.displayError(response.data);
        }

        loadEmployee();

    }

})(angular.module('companyStore'));