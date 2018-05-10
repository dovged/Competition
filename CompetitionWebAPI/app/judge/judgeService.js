'use strict';
app.factory('judgeService', ['$http', 'authService', function ($http, authService) {

    var serviceBase = 'http://localhost:52336/';
    var judgeServiceFactory = {};
    var _user = authService.authentication.userName;

    // Gaunamas varžybų sąrašas kurioms gali vartotojas pildyti teisejo lapa
    var _getCompetitionList = function () {
        return $http.get(serviceBase + 'api/judgeCompetitions/' + _user).then(function (results) {
            return results;
        });
    };

    // Gaunamas dalyvių sąrašas
    var _getUserList = function (compId) {
        return $http.get(serviceBase + 'api/competitionsclim/' + compId + '/1').then(function (results) {
            return results;
        });
    };

    // Gaunamas trasų sąrašas, pagal grupę
    var _getRouteListType = function (compId, type) {
        return $http.get(serviceBase + 'api/routeClim/' + compId + '/' + type).then(function (results) {
            return results;
        });
    };

    // Gaunamas dalyvių sąrašas
    var _getUserListGroup = function (compId, group) {
        return $http.get(serviceBase + 'api/competitionsclim/' + compId + '/1/' + group).then(function (results) {
            return results;
        });
    };

    // Pildyti teisėjo lapą
    var _updateJudgesPaper = function (routeId, userId, id) {
        var updaterequest = $http({
            method: 'put',
            url: serviceBase + 'api/judgespaperClim/' + routeId + '/'+ userId + '/' + id,
        });

        return updaterequest;
    };


    // Gaunamas teisejo lapas
    var _getJudgePaper = function (routeId, userId) {
        return $http.get(serviceBase + 'api/judgespapersClim/' + routeId + '/' + userId + '/1').then(function (results) {
            return results;
        });
    };
    /** PRISKIRIMAI */
    judgeServiceFactory.getCompetitionList = _getCompetitionList;
    judgeServiceFactory.getUserList = _getUserList;
    judgeServiceFactory.updateJudgesPaper = _updateJudgesPaper;
    judgeServiceFactory.getRouteListType = _getRouteListType;

    return judgeServiceFactory;

}]);