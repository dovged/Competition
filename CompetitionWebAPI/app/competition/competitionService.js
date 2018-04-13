﻿'use strict';
app.factory('competitionService', ['$http', 'authService', 'localStorageService', function ($http, authService, localStorageService) {

    var serviceBase = 'http://localhost:52336/';
    var competitionServiceFactory = {};
    var _user = authService.authentication.userName;

    /** Grąžinamas sąrašas varžybų, pagal ORG  */
    var _getCompetitionList = function () {
       return $http.get(serviceBase + 'api/competition/' + _user + '/1').then(function (results) {
            return results;
        });
    };

    /** Vienų varžybų info pagal ID*/
    var _getCompetitionDetails = function (Id) {
        return $http.get(serviceBase + 'api/competition/' + Id).then(function (results) {
            return results;
        });
    };

    // Nesusimokėję dalyviai LAIPIOJIMAS, vienos varžybos pagal ID
    var _getNonPaidClim = function (Id) {
        return $http.get(serviceBase + 'api/competitionsclimNonPaid/' + Id + '/1').then(function (results) {
            return results;
        });
    };

    // Nesusimokėję dalyviai KKT, vienos varžybos pagal ID
    var _getNonPaidKKT = function (Id) {
        return $http.get(serviceBase + 'api/competitionsKKTNonPaid/' + Id + '/1').then(function (results) {
            return results;
        });
    };

    // Gauti KKT varžybų trasų info
    var _getKKTRoutes = function (Id) {
        return $http.get(serviceBase + 'api/routeKKT/' + Id).then(function (results) {
            return results;
        });
    };

    /* Pažymimas dalyvis, kuris susimokėjo KKT*/
    var _paidKKT = function (Id) {
        var updaterequest = $http({
            method: 'put',
            url: serviceBase + "api/competitionKKTPaid/" + Id,
        });

        return updaterequest;
    };

    /* Pažymimas dalyvis, kuris susimokėjo LAIPIOJIMAS*/
    var _paidClim = function (Id) {
        var updaterequest = $http({
            method: 'put',
            url: serviceBase + "api/competitionClimPaid/" + Id,
        });

        return updaterequest;
    };

    /** Patikrinama ar organizatorius yra KKT organizatorius*/
    var _KKTOrg = function () {
        return $http.get(serviceBase + 'api/role/' + _user + '/6').then(function (results) {
            return results;
        });
    };


    /** PRISKIRIMAI */
    competitionServiceFactory.getCompetitionList = _getCompetitionList;
    competitionServiceFactory.getCompetitionDetails = _getCompetitionDetails;
    competitionServiceFactory.getNonPaidClim = _getNonPaidClim;
    competitionServiceFactory.getNonPaidKKT = _getNonPaidKKT;
    competitionServiceFactory.getKKTRoutes = _getKKTRoutes;
    competitionServiceFactory.paidKKT = _paidKKT;
    competitionServiceFactory.paidClim = _paidClim;
    competitionServiceFactory.KKTOrg = _KKTOrg;

    return competitionServiceFactory;

}]);