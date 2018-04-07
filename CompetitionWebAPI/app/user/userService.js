'use strict';
app.factory('userService',['$http', 'authService', function ($http, authService) {

    var serviceBase = 'http://localhost:52336/';
    var userServiceFactory = {};
    var _user = authService.authentication.userName;

    // Grąžinti sąrašą
    var _getUserTeam = function () {

        return $http.get(serviceBase + "api/team/" + _user + "/1").then(function (results) {
            return results;
        });
    };

    /* Grąžinamas sąrašas varžybų į kurias užsiregistravo dalyvis LAIPIOJIMAS*/
    var _getCompListClim = function () {
        return $http.get(serviceBase + "api/clim/" + _user + "/1").then(function (results) {
            return results;
        });
    };

     /* Grąžinamas sąrašas varžybų į kurias užsiregistravo dalyvis KKT*/
    var _getCompListKKT = function () {
        return $http.get(serviceBase + "api/climKKT/" + _user + "/1").then(function (results) {
            return results;
        });
    };

    // Panaikinama varžybų registracija LAIPIOJIMAS
    var _deleteCompClim = function (Id) {
        var deleterequest = $http({
            method: 'delete',
            url: serviceBase + "api/clim/" + Id
        });
        return deleterequest;
    }

    // Painaikinama varžybų registracija KKT
    var _deleteCompKKT = function (Id) {
        var deleterequest = $http({
            method: 'delete',
            url: serviceBase + "api/climKKT/" + Id
        });
        return deleterequest;
    }


    userServiceFactory.getUserTeam = _getUserTeam;
    userServiceFactory.getCompListClim = _getCompListClim;
    userServiceFactory.getCompListKKT = _getCompListKKT;
    userServiceFactory.deleteCompClim = _deleteCompClim;
    userServiceFactory.deleteCompKKT = _deleteCompKKT;

    return userServiceFactory;

}]);