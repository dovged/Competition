'use strict';
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

    // Gauti KKT varžybų trasos info
    var _getKKTRouteInfo = function (Id) {
        return $http.get(serviceBase + 'api/competition/1/routeKKT/' + Id).then(function (results) {
            return results;
        });
    };

    // Gauti KKT varžybų trasų info
    var _getKKTRoutes = function (Id) {
        return $http.get(serviceBase + 'api/routeKKT/' + Id).then(function (results) {
            return results;
        });
    };

    // Gauti teisėjų sąrašą
    var _getJudges = function (Id) {
        return $http.get(serviceBase + 'api/compJudge/' + Id).then(function (results) {
            return results;
        });
    };

    // Gauti visų vartotojų sąrašą, naudojamas pridedant naują teisėją
    var _getUsers = function () {
        return $http.get(serviceBase + 'api/user').then(function (results) {
            return results;
        });
    };

    /** Pridedamas vartotojas į teisėjų sąrašą*/
    var _addJudge = function (compId, id) {
        var addrequest = $http({
            method: 'post',
            url: serviceBase + "api/compJudge/" + compId + "/" + id,
        });

        return addrequest;
    };

    /** Pridedama nauja KKT trasa*/
    var _addKKTRoute = function (r) {
        var request = $http({
            method: 'post',
            url: serviceBase + "api/routeKKT",
            data: r
        });
        return request;
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

    /** Atnaujinama KKT trasos informacija*/
    var _updateKKTRoute = function (Id, r) {
        var request = $http({
            method: 'put',
            url: serviceBase + "api/routeKKT/" + Id,
            data: r
        });
        return request;
    };

    /** Panaikinamas teisėjas iš sąrašo*/
    var _deleteJudge = function (Id) {
        var deleterequest = $http({
            method: 'delte',
            url: serviceBase + "api/compJudge/" + Id,
        });

        return deleterequest;
    };

    // Gaunamas sąrašas vartotojų pagal Org klubą, reikalinga, varžybų duomenims atnaujinti
    var _getUsersClub = function () {
        return $http.get(serviceBase + 'api/userClub/1/' + _user).then(function (results) {
            return results;
        });
    };

    /** Atnaujinti varžybų informaciją*/
    var _updateCompInfo = function (Id, c) {
        var updaterequest = $http({
            method: 'put',
            url: serviceBase + "api/competition/" + Id,
            data: c
        });

        return updaterequest;
    };

    // Pridėti naują baudą
    var _addCompetition = function (Comp) {
        var request = $http({
            method: 'post',
            url: serviceBase + "api/competition/" + _user,
            data: comp
        });
        return request;
    }

    /** PRISKIRIMAI */
    competitionServiceFactory.getCompetitionList = _getCompetitionList;
    competitionServiceFactory.getCompetitionDetails = _getCompetitionDetails;
    competitionServiceFactory.getNonPaidClim = _getNonPaidClim;
    competitionServiceFactory.getNonPaidKKT = _getNonPaidKKT;
    competitionServiceFactory.getKKTRoutes = _getKKTRoutes;
    competitionServiceFactory.paidKKT = _paidKKT;
    competitionServiceFactory.paidClim = _paidClim;
    competitionServiceFactory.getJudges = _getJudges;
    competitionServiceFactory.getUsers = _getUsers;
    competitionServiceFactory.deleteJudge = _deleteJudge;
    competitionServiceFactory.addJudge = _addJudge;
    competitionServiceFactory.getKKTRouteInfo = _getKKTRouteInfo;
    competitionServiceFactory.updateKKTRoute = _updateKKTRoute;
    competitionServiceFactory.addKKTRoute = _addKKTRoute;
    competitionServiceFactory.getUsersClub = _getUsersClub;
    competitionServiceFactory.updateCompInfo = _updateCompInfo;
    competitionServiceFactory.addCompetition = _addCompetition;

    

    return competitionServiceFactory;

}]);