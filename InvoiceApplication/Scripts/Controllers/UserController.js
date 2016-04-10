var UserController = function ($scope, $http) {

    //promise functions.
    var SuccessGetCallback = function (response) {
        $scope.users = response.data;
    }
    var successCallback = function (response) {
        $scope.user = "";
        $scope.error = 0;
        $scope.complete = true;
    };
    var errorCallback = function (response) {
        $scope.error = 1;
    };

    //$scope.user databinds to the add user form.
    $scope.user = {
        name: '',
        type: 'user',
    };
    //error count check.
    $scope.error = 0;
    //show success message if true.
    $scope.complete = false;
    //$scope.users is the users collection does not need to be loaded for every page that uses 
    //this controller.  This needs attention to reduce the amount of calls.  
    //maybe a directive of <userList> and wrap the functionality in there.  
    $scope.users = [];
    $http.get('/User/GetUserList').then(SuccessGetCallback, errorCallback);

    //function to use when we go and submit a user.
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


//inject our dependencies.
UserController.$inject = ['$scope', '$http'];