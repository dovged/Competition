'use strict';
app.factory('trainerService', ['$http', 'authService', function ($http, authService) {

    var serviceBase = 'http://localhost:52336/';
    var trainerServiceFactory = {};
    var _user = authService.authentication.userName;

    // Gaunamas treniruojamų dalyvių sąrašas
    var _getUserList = function () {
        return $http.get(serviceBase + 'api/user/' + _user + '/1').then(function (results) {
            return results;
        });
    };

    // Treneris užregistruoja naują dalyvį
    var _addTrainee = function (t) {
        var addrequest = $http({
            method: 'post',
            url: serviceBase + "api/userTrainee/" + _user,
            data: t
        });
    };

    // Gaunamas varžybų sąrašas į kurias gali registruotis jaunieji sportininkai
    var _getCompetitionListTrainer = function () {
        return $http.get(serviceBase + 'api/competitionKids/' + _user).then(function (results) {
            return results;
        });
    };

    // Gaunamas dalyvių sąrašas LAIPIOJIMAS
    var _getRegisterClim = function (compId) {
        return $http.get(serviceBase + "api/compKidsClim/" + compId + "/" + _user + "/1").then(function (results) {
            return results;
        });
    };

    // Užregistruojamas dalyvis į varžybas LAIPIOJIMAS
    var _addRegisterClim = function (compId, climberId) {
        var addrequest = $http({
            method: 'post',
            url: serviceBase + "api/competition/" + compId + "/climKids/" + climberId
        });

        return addrequest;
    };

    // Išregistruojamas dalyvis iš varžybų LAIPIOJIMAS
    var _removeRegisterClim = function (compId, climberId) {
        var removerequest = $http({
            method: 'delete',
            url: serviceBase + "api/clim/" + compId + "/" + climberId
        });

        return removerequest;
    };

    // Gaunamas dalyvių sąrašas KKT
    var _getRegisterKKT = function (compId) {
        return $http.get(serviceBase + "api/compKidsKKT/" + compId + "/" + _user + "/1").then(function (results) {
            return results;
        });
    };

    // Užregistruojamas dalyvis į varžybas KKT
    var _addRegisterKKT = function (compId, teamId) {
        var addrequest = $http({
            method: 'post',
            url: serviceBase + "api/competition/" + compId + "/climKidsKKT/" + teamId
        });

        return addrequest;
    };

    // Išregistruojamas dalyvis iš varžybų KKT
    var _removeRegisterKKT = function (compId, teamId) {
        var removerequest = $http({
            method: 'delete',
            url: serviceBase + "api/climKKT/" + compId + "/" + teamId
        });

        return removerequest;
    };

    // Gaunama varžybų informacija
    var _getCompInfo = function (compId) {
        return $http.get(serviceBase + "api/competition/" + compId).then(function (results) {
            return results;
        });
    };

    /** PRISKIRIMAI */
    trainerServiceFactory.getUserList = _getUserList;
    trainerServiceFactory.addTrainee = _addTrainee;
    trainerServiceFactory.getCompetitionListTrainer = _getCompetitionListTrainer;
    trainerServiceFactory.getRegisterClim = _getRegisterClim;
    trainerServiceFactory.addRegisterClim = _addRegisterClim;
    trainerServiceFactory.removeRegisterClim = _removeRegisterClim;
    trainerServiceFactory.getRegisterKKT = _getRegisterKKT;
    trainerServiceFactory.addRegisterKKT = _addRegisterKKT;
    trainerServiceFactory.removeRegisterKKT = _removeRegisterKKT;
    trainerServiceFactory.getCompInfo = _getCompInfo;

    return trainerServiceFactory;

}]);