var InvoiceApplication = angular.module('InvoiceApplication', ['ngRoute']);

InvoiceApplication.controller('LandingPageController', LandingPageController);

var configFunction = function ($routeProvider, $httpProvider, $locationProvider) {
    $locationProvider.hashPrefix('!').html5Mode(true);

    $routeProvider
        .when("/routeOne", {
            templateUrl: 'routesDemo/one'
        })
        .when("/routeTwo/:donuts", {
            templateUrl: function (params) { return '/routesDemo/two?donuts=' + params.donuts;}
        })
        .when("/routeThree", {
            templateUrl: 'routesDemo/three'
        });

}

configFunction.$inject = ["$routeProvider","$httpProvider", "$locationProvider"];

InvoiceApplication.config(configFunction);