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

    /** Visų varžybų sąrašas*/
    var _getCompetitionListAll = function () {

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

    competitionServiceFactory.getCompetitionList = _getCompetitionList;
    competitionServiceFactory.getCompetitionListAll = _getCompetitionListAll;
    competitionServiceFactory.getCompetitionDetails = _getCompetitionDetails;
    competitionServiceFactory.getNonPaidClim = _getNonPaidClim;
    competitionServiceFactory.getNonPaidKKT = _getNonPaidKKT;

    return competitionServiceFactory;

}]);