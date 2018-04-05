'use strict';
app.controller('adminController', ['$scope', 'adminService', function ($scope, adminService) {

    $scope.userList = [];
    loadUserList();

    // Užkraunami duomenys į lentelę
    function loadUserList() {
        adminService.getUserList().then(function (results) {

            $scope.userList = results.data;

        }, function (error) {
            //alert(error.data.message);
        });
    }

    $scope.removeRole = function (Id) {
        adminService.removeRole(Id).then(function (results) {
            loadUserList();
        });
    }

    $scope.removeUser = function (Id) {
        adminService.removeUser(Id).then(function (results) {
            loadUserList();
        });
    }

}]);