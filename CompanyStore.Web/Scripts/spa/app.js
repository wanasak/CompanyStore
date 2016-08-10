(function () {
    'use strict';

    angular.module('companyStore', ['common.ui', 'common.core'])
        .config(config)
        .run(run);

    config.$inject = ['$routeProvider'];

    function config($routeProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "scripts/spa/home/index.html",
                controller: "indexCtrl"
            })
            .when("/login", {
                templateUrl: "scripts/spa/account/login.html",
                controller: "loginCtrl"
            })
            .when("/register", {
                templateUrl: "scripts/spa/account/register.html",
                controller: "registerCtrl"
            })
            .when("/employee", {
                templateUrl: "scripts/spa/employee/employee.html",
                controller: "employeeCtrl"
            })
            .when("/employee/add", {
                templateUrl: "scripts/spa/employee/employeeAdd.html",
                controller: "employeeAddCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/device", {
                templateUrl: "scripts/spa/device/device.html",
                controller: "deviceCtrl"
            })
            .when("/device/detail/:id", {
                templateUrl: "scripts/spa/device/deviceDetail.html",
                controller: "deviceDetailCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/device/add", {
                templateUrl: "scripts/spa/device/deviceAdd.html",
                controller: "deviceAddCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/rental", {
                templateUrl: "scripts/spa/rental/rental.html",
                controller: "rentalCtrl"
            })
            .otherwise({ redirectTo: "/" });
    }

    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http'];

    function run($rootScope, $location, $cookieStore, $http) {
        // Handle page refreshs
        $rootScope.repository = $cookieStore.get('repository') || {};
        if ($rootScope.repository.loggedUser) {
            //$http.defaults.headers.common['Authorization'] = $rootScope.repository.loggedUser.authdata;
            $http.defaults.headers.common['Authorization'] = 'Basic ' + $rootScope.repository.loggedUser.authData;
        }
        $(document).ready(function () {
            $(".fancybox").fancybox({
                openEffect: 'none',
                closeEffect: 'none'
            });
            $('.fancybox-media').fancybox({
                openEffect: 'none',
                closeEffect: 'none',
                helpers: { media: {} }
            });
            $('[data-toggle=offcanvas]').click(function () {
                $('.row-offcanvas').toggleClass('active');
            });
        });
    }

    isAuthenticated.$inject = ['membershipService', '$rootScope', '$location'];

    function isAuthenticated(membershipService, $rootScope, $location) {
        if (!membershipService.isUserLoggedIn()) {
            $rootScope.previousState = $location.path();
            $location.path('/login');
        }
    }


})();