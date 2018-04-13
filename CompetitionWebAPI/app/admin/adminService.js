'use strict';
app.factory('adminService', function ($http) {

    var serviceBase = 'http://localhost:52336/';
    var adminServiceFactory = {};

    // Grąžinti vartotojų sąrašą
    var _getUserList = function () {

        return $http.get(serviceBase + "api/user").then(function (results) {
            return results;
        });
    };

    // Gaunamas rolių sąrašas
    var _getRoleList = function () {
        return $http.get(serviceBase + "api/role").then(function (results) {
            return results;
        });
    };

    //Panaikinama vartotojo rolė
    var _removeRole = function (Id) {
        var deleteRequest = $http({
            method: 'delete',
            url: serviceBase + "api/userRole/" + Id
        });

        return deleteRequest;
    }

    // Pridedama vartotojui rolė
    var _addRole = function (RoleId, UserId) {
        var addRequest = $http({
            method: 'post',
            url: serviceBase + "api/role/" + RoleId + "/" + UserId
        });

        return addRequest;
    }

    // Padaromas vartotojas neaktyviu
    var _removeUser = function (Id) {
        var deleteRequest = $http({
            method: 'delete',
            url: serviceBase + "api/user/" + Id
        });

        return deleteRequest;
    }

    /** PRISKIRIMAI*/
    adminServiceFactory.getUserList = _getUserList;
    adminServiceFactory.removeRole = _removeRole;
    adminServiceFactory.removeUser = _removeUser;
    adminServiceFactory.getRoleList = _getRoleList;
    adminServiceFactory.addRole = _addRole;

    return adminServiceFactory;
});