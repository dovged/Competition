'use strict';
app.controller('adminController', ['$scope', 'adminService', function ($scope, adminService) {

    $scope.userList = [];
    loadUserList();
    $scope.openModalAdd = false;

    // Užkraunami duomenys į lentelę
    function loadUserList() {
        adminService.getUserList().then(function (results) {

            $scope.userList = results.data;

        }, function (error) {
            //alert(error.data.message);
        });
    }

    $scope.AddRole = function () {
        $scope.openModalAdd = true;
    }

    $scope.removeRole = function (Id) {
        adminService.removeRole(Id).then(function (results) {
            loadUserList();
        });
    }

}]);