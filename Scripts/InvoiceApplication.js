var InvoiceApplication = angular.module('InvoiceApplication', ['ngRoute']);

InvoiceApplication.controller('LandingPageController', LandingPageController);

var configFunction = function ($routeProvider, $httpProvider, $locationProvider) {
    $locationProvider.hashPrefix('!').html5Mode({enabled: true, requireBase: false});

    $routeProvider
        .when("/users", {
            templateUrl: 'routes/AddForm'
        })
        .when("/pages", {
            templateUrl: "routes/PageList"
        })
        .when("/invoice", {
            templateUrl: 'routes/InvoiceList'
        });

}

configFunction.$inject = ["$routeProvider","$httpProvider", "$locationProvider"];

InvoiceApplication.config(configFunction);