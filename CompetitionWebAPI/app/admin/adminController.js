'use strict';
app.controller('adminController', ['$scope', 'adminService', function ($scope, adminService) {

    $scope.userList = {};
    $scope.roleList = {};
    $scope.newRole = {
        RoleId: '',
        UserId: ''
    };

    loadUserList();

    // Užkraunami duomenys į lentelę
    function loadUserList() {

        // Gaunamas aktyvių varotojų sąrašas
        adminService.getUserList().then(function (results) {
            $scope.userList = results.data;
        });
        // Gaunamas rolių sąrašas
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
    };

    // Pridedama nauja rolė vartotojui
    $scope.addRole = function (userId) {
        var userRole = {
            RoleId: $scope.newRole.RoleId,
            UserId: userId
        };
        adminService.addRole(userRole).then(function (results) {
            loadUserList();
        });
    };

}]);