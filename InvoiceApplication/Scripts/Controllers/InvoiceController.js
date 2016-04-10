//controller used for all things invoice.
var InvoiceController = function ($scope, $http) {

    //promise functions.
    var PopulateUsers = function (response) {

        $scope.chargeTo.users = response.data;
    };
    var PopulateInvoices = function (response) {

        $scope.invoices = response.data;
    };

    var SuccessGetCallback = function (response) {
        $scope.invoices = response.data;
    };
    var successCallback = function (response) {
        $scope.chargeTo.charge = "";
        $scope.charges.bill = 0.0;
        $scope.charges.description = "";
        $scope.complete = true;

    };
    var errorCallback = function (response) {
        $scope.error = 1;
    };


    //success message.
    $scope.complete = false;

    //a collection of invoices for use with the invoice list.
    $scope.invoices = [];

    //$scope.chargeTo is who we are charging to. it holds a collection of users for 
    //the drop down on the invoice form and writes the user ID to the charge property when selected.
    $scope.chargeTo = {
        users : [],
        charge : ""
    };

    //$scope.charges object has bill: which is the billed amount to the customer, 
    //and description: which is the description of the charge.
    $scope.charges = {
        bill: 0.0,
        description: ""
    };
    //initial setup.  these dont all need to run each time the controller starts. upgrade needed
    //similar to the user controller.
    $http.get('/Invoice/Get/All').then(PopulateInvoices, errorCallback);
    $http.get('/User/GetUserList').then(PopulateUsers, errorCallback);
    $http.get('/Invoice/Get/All').then(SuccessGetCallback, errorCallback);

    //function used when we submit an invoice.
    $scope.submitInvoice = function () {
        if ($scope.charges.description && $scope.chargeTo.charge) {
            var data = {
                charges: $scope.charges.bill + "",
                chargeDesc: $scope.charges.description,
                chargeID: $scope.chargeTo.charge

            };
            $http.post('/Invoice/Add', data).then(successCallback, errorCallback);
        } else {
            $scope.error++;
            $scope.complete = false;
        }

    }
    
}

// The $inject property of every controller (and pretty much every other type of object in Angular) needs to be a string array equal to the controllers arguments, only as strings
InvoiceController.$inject = ['$scope', '$http'];