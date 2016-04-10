var UserController = function ($scope, $http) {
    $scope.user = {
        name: 'blahblah',
        type: 'user',
    };
    $scope.error = 0;
    $scope.complete = false;
    $scope.users = [];
    //data bind to the properties in the form.
   
    var SuccessGetCallback = function (response) {
        console.log(response);
        $scope.users = response.data;
        console.log($scope.users);
    }
    var successCallback = function (response) {
        $scope.user = "";
        $scope.error = 0;
        $scope.complete = true;
    };
    var errorCallback = function (response) {
        $scope.error = 1;
        console.error(response);
    };
    $http.get('/User/GetUserList').then(SuccessGetCallback, errorCallback);
    $scope.submitUser = function () {
        if ($scope.user && $scope.user.name) {
            var data = {
                ID: Date.now,
                name: $scope.user.name,
                type: $scope.user.type
            };
            $http.post('/User/Add', data).then(successCallback, errorCallback);
        } else {
            $scope.error++;
            $scope.complete = false;
        }
    }
}




// The $inject property of every controller (and pretty much every other type of object in Angular) needs to be a string array equal to the controllers arguments, only as strings
UserController.$inject = ['$scope'];