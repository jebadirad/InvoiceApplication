//simple landing page controller.  
var HomeController = function ($scope) {
    $scope.models = {
        helloAngular: 'Angular MVC Demo'
    };
}

//inject our dependencies.
HomeController.$inject = ['$scope'];