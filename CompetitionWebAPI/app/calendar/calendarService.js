'use strict';
app.factory('calendarService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:52336/';
    var calendarServiceFactory = {};

    // Gaunamas varžybų sąrašas
    var _getCompetitionList = function () {
        return $http.get(serviceBase + 'api/competition/').then(function (results) {
            return results;
        });
    };

    /** PRISKIRIMAI*/
    calendarServiceFactory.getCompetitionList = _getCompetitionList;

    return calendarServiceFactory;

}]);