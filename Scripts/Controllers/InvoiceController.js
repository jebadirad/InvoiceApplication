var InvoiceController = function ($scope, $http) {
    $scope.invoices = [];
    //data bind to the properties in the form.
    $scope.chargeTo = {
        users : [],
        charge : ""
    };
    $scope.complete = false;
    $scope.charges = {
        bill: 0.0,
        description : ""
    };
    var PopulateUsers = function (response) {
        
        $scope.chargeTo.users = response.data;
    };
    var PopulateInvoices = function (response) {

        $scope.invoices = response.data;
    };
    $http.get('/Invoice/Get/All').then(PopulateInvoices, errorCallback);
    $http.get('/User/GetUserList').then(PopulateUsers, errorCallback);
    var SuccessGetCallback = function (response) {
        console.log(response);
        $scope.invoices = response.data;
        console.log($scope.users);
    };
    var successCallback = function (response) {
        $scope.chargeTo.charge = "";
        $scope.charges.bill = 0.0;
        $scope.charges.description = "";
        $scope.complete = true;

    };
    var errorCallback = function (response) {
        $scope.error = 1;
        console.error(response);
    };
    $http.get('/Invoice/Get/All').then(SuccessGetCallback, errorCallback);
    $scope.submitInvoice = function () {
        if ($scope.charges.description && $scope.chargeTo.charge) {
            var data = {
                charges: $scope.charges.bill + "",
                chargeDesc: $scope.charges.description,
                chargeID : $scope.chargeTo.charge

            };
            $http.post('/Invoice/Add', data).then(successCallback, errorCallback);
        } else {
            $scope.error++;
            $scope.complete = false;
        }
     
    }
}

// The $inject property of every controller (and pretty much every other type of object in Angular) needs to be a string array equal to the controllers arguments, only as strings
InvoiceController.$inject = ['$scope'];