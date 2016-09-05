(function (app) {
    'use strict';

    app.controller('chartCtrl', chartCtrl);

    chartCtrl.$inject = ['$scope', 'apiService'];

    function chartCtrl($scope, apiService) {

        // Morris Chart
        var departmentDouut = null;

        $scope.departments = [];

        function loadDepartmentDonut() {
            apiService.get("api/department/employee/chart", null,
                loadDepartmentDonutCompleted,
                loadDepartmentDonutFailed)
        }
        function loadDepartmentDonutCompleted(result) {
            $scope.departments = result.data;
            if (!departmentDouut) {
                departmentDouut = Morris.Donut({
                    element: "department-donut",
                    data: result.data,
                    formatter: function (y, data) { return 'Number of Employees: ' + y },
                    resize: true
                    //colors: ['#0000ff', '#009933', '#ffff00', '#ff0000', '#0099ff', '#999966', '#cc00cc']
                });
            } else {
                departmentDouut.setData(result.data);
            }
        }
        function loadDepartmentDonutFailed(response) { }

        loadDepartmentDonut();
    }

})(angular.module('companyStore'));