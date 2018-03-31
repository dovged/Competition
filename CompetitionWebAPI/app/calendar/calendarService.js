'use strict';
app.factory('calendarService', ['$http', 'authService', function ($http, authService) {

    var serviceBase = 'http://localhost:52336/';
    var calendarServiceFactory = {};
    var user = authService.authentication.userName;

    var _getCompetitionList = function () {

        /* return $http.get(serviceBase + 'api/competition/' + user + '/1').then(function (results) {
             return results;
         });*/
        return $http.get(serviceBase + 'api/competition/').then(function (results) {
            return results;
        });
    };

    var _getCompetitionListAll = function () {

        return $http.get(serviceBase + 'api/competition/').then(function (results) {
            return results;
        });
    };

    calendarServiceFactory.getCompetitionList = _getCompetitionList;
    calendarServiceFactory.getCompetitionListAll = _getCompetitionListAll;

    return calendarServiceFactory;

}]);