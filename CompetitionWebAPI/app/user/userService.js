'use strict';
app.factory('userService',['$http', 'authService', function ($http, authService) {

    var serviceBase = 'http://localhost:52336/';
    var userServiceFactory = {};
    var _user = authService.authentication.userName;

    // Grąžinti vartotojo komandos informaciją
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

    //Gaunas klubų sąrašas
    var _getClubs = function () {
        return $http.get(serviceBase + "api/club").then(function (results) {
            return results;
        });
    };

    // Gauna vartotojo inforamcija
    var _getUser = function () {
        return $http.get(serviceBase + "api/user/" + _user).then(function (results) {
            return results;
        });
    };

    //Atnaujinama vartotojo informacija
    var _updateUser = function (u) {
        var updaterequest = $http({
            method: 'put',
            url: serviceBase + "api/user/" + _user,
            data: u
        });
    };

    // Ištrinamas narys iš komandos
    var _removeMember = function (id) {
        var deleterequest = $http({
            method: 'put',
            url: serviceBase + "api/removeMember/" + id
        });

        return deleterequest;
    };

    // Pirdėti dalyvį į komandą
    var _addMember = function (id, teamid) {
        var addrequest = $http({
            method: 'put',
            url: serviceBase + "api/addMember/" + id + "/" + teamid
        });

        return addrequest;
    }

    // Pridėti komandą
    var _addTeam = function (team) {
        var addrequest = $http({
            method: 'put',
            url: serviceBase + "api/team",
            data: team
        });

        return addrequest;
    };

    // Gaunamas vartotojų be KKT komandos sąrašas
    var _getUserNoTeam = function () {
        return $http.get(serviceBase + "api/userNoTeam/1").then(function (results) {
            return results;
        });
    };


    // PRISIKIRIMAI
    userServiceFactory.getUserTeam = _getUserTeam;
    userServiceFactory.getCompListClim = _getCompListClim;
    userServiceFactory.getCompListKKT = _getCompListKKT;
    userServiceFactory.deleteCompClim = _deleteCompClim;
    userServiceFactory.deleteCompKKT = _deleteCompKKT;
    userServiceFactory.getClubs = _getClubs;
    userServiceFactory.getUser = _getUser;
    userServiceFactory.updateUser = _updateUser;
    userServiceFactory.removeMember = _removeMember;
    userServiceFactory.addMember = _addMember;
    userServiceFactory.addTeam = _addTeam;
    userServiceFactory.getUserNoTeam = _getUserNoTeam;

    return userServiceFactory;

}]);