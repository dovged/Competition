'use strict';
app.controller('adminController', ['$scope', 'adminService', function ($scope, adminService) {

    $scope.userList = {};
    $scope.roleList = {};
    $scope.newRole = false;
    $scope.newRoleId = 0;

    loadUserList();

    // Užkraunami duomenys į lentelę
    function loadUserList() {
        adminService.getUserList().then(function (results) {
            $scope.userList = results.data;
        });
        adminService.getRoleList().then(function (results) {
            $scope.roleList = results.data;
        });
    };

    // Panaikinama vartotojui rolė
    $scope.removeRole = function (Id) {
        adminService.removeRole(Id).then(function (results) {
            loadUserList();
        });
    }

    // Vartotojas padaromas neaktyviu
    $scope.removeUser = function (Id) {
        adminService.removeUser(Id).then(function (results) {
            loadUserList();
        });
    }

    // Gaunamas rolių sąrašas
    $scope.getRoleList = function () {
        adminService.getRoleList().then(function (results) {
            $scope.roleList = results.data;
        });
        $scope.newRole = true;
    };

    // Pridedama nauja rolė vartotojui
    $scope.addRole = function (userId) {
        adminService.addRole($scope.newRoleId, userId).then(function (results) {
            loadUserList();
        });
        $scope.newRole = false;
    };

}]);