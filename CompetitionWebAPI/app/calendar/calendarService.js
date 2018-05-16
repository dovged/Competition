'use strict';
app.factory('calendarService', ['$http', 'authService', function ($http, authService,) {

    var serviceBase = 'http://localhost:52336/';
    var calendarServiceFactory = {};
    var _user = authService.authentication.userName;

    // Gaunamas varžybų sąrašas
    var _getCompetitionList = function () {
        return $http.get(serviceBase + 'api/competition/').then(function (results) {
            return results;
        });
    };

    /** Vienų varžybų info pagal ID*/
    var _getCompetitionDetails = function (Id) {
        return $http.get(serviceBase + 'api/competition/' + Id).then(function (results) {
            return results;
        });
    };

    // Užregistruojamas dalyvis į KKT varžybas
    var _addCompetitorKKT = function (c, compId) {
        var request = $http({
            method: 'post',
            url: serviceBase + "api/competitionKKT/" + compId + "/KKT/" + _user,
            data: c
        });
        return request;
    };

    // Užregistruojamas dalyvis į LAIPIOJIMO varžybas
    var _addCompetitorClim = function (c, compId) {
        var request = $http({
            method: 'post',
            url: serviceBase + "api/competition/" + compId + "/clim/" + _user,
            data: c
        });
        return request;
    };

    // Gauti rezultatus
    var _getResults = function (compId, resultType, lytis, group) {
        return $http.get(serviceBase + 'api/results/' + compId + '/' + resultType + '/' + lytis +'/' + group).then(function (results) {
            return results;
        });
    };

    /** PRISKIRIMAI*/
    calendarServiceFactory.getCompetitionList = _getCompetitionList;
    calendarServiceFactory.addCompetitorKKT = _addCompetitorKKT;
    calendarServiceFactory.getCompetitionDetails = _getCompetitionDetails;
    calendarServiceFactory.addCompetitorClim = _addCompetitorClim;
    calendarServiceFactory.getResults = _getResults;


    return calendarServiceFactory;

}]);