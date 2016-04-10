//define our angular application.
var InvoiceApplication = angular.module('InvoiceApplication', ['ngRoute']);

//assign our controlelrs and dependencies.
InvoiceApplication.controller('HomeController', HomeController);
InvoiceApplication.controller('UserController', ["$scope", "$http", UserController]);
InvoiceApplication.controller('InvoiceController', ["$scope", "$http", InvoiceController]);

//route configuration.
var configFunction = function ($routeProvider, $httpProvider, $locationProvider) {
    //remove the #
    $locationProvider.hashPrefix('!').html5Mode({ enabled: true, requireBase: false });

    //setup the route provider to forward route requests to mvc and let .net take over and return our view.
    $routeProvider
        .when("/user", {
            templateUrl: 'routes/User'
        })
        .when("/invoice", {
            templateUrl: 'routes/InvoiceList'
        })
        .when("/invoice/add", {
            templateUrl: '/routes/Invoice/Add'
        })
        .when("/invoice/view/:id", {
            templateUrl: function (params) {
                return '/routes/Invoice/view?id=' + params.id;
            }
        })
        .when("/user/add", {
            templateUrl: '/routes/User/Add'
        });
}
//inject into our configuration.  
configFunction.$inject = ["$routeProvider", "$httpProvider", "$locationProvider"];

//set the config
InvoiceApplication.config(configFunction);