'use strict';
app.factory('competitionService', ['$http', 'authService', function ($http, authService) {

    var serviceBase = 'http://localhost:52336/';
    var competitionServiceFactory = {};
    var user = authService.authentication.userName;

    var _getCompetitionList = function () {

        return $http.get(serviceBase + 'api/competition/' + user + '/1').then(function (results) {
            return results;
        });
    };

    competitionServiceFactory.getCompetitionList = _getCompetitionList;

    return competitionServiceFactory;

}]);