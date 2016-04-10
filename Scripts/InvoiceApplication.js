var InvoiceApplication = angular.module('InvoiceApplication', ['ngRoute']);

InvoiceApplication.controller('HomeController', HomeController);
InvoiceApplication.controller('UserController', ["$scope", "$http", UserController]);
var configFunction = function ($routeProvider, $httpProvider, $locationProvider) {
    $locationProvider.hashPrefix('!').html5Mode({ enabled: true, requireBase: false });

    $routeProvider
        .when("/user", {
            templateUrl: 'routes/User'
        })
        .when("/invoice", {
            templateUrl: 'routes/InvoiceList'
        })
        .when("/user/add", {
            templateUrl: '/routes/User/Add'
        });
    //user/routes/User/Add
}

configFunction.$inject = ["$routeProvider", "$httpProvider", "$locationProvider"];

InvoiceApplication.config(configFunction);