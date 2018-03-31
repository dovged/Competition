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


    adminServiceFactory.getUserList = _getUserList;

    return adminServiceFactory;

});