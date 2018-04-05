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

    var _removeRole = function (Id) {
        var deleteRequest = $http({
            method: 'delete',
            url: serviceBase + "api/userRole/" + Id
        });

        return deleteRequest;
    }

    var _removeUser = function (Id) {
        var deleteRequest = $http({
            method: 'delete',
            url: serviceBase + "api/user/" + Id
        });

        return deleteRequest;
    }


    adminServiceFactory.getUserList = _getUserList;
    adminServiceFactory.removeRole = _removeRole;
    adminServiceFactory.removeUser = _removeUser;

    return adminServiceFactory;

});