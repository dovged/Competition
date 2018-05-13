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

    // LAIPIOJIMO VARŽYBOMS NAUDOJAMI METODAI

    // Gaunamas dalyvių sąrašas
    var _getUserList = function (compId) {
        return $http.get(serviceBase + 'api/competitionsclim/' + compId + '/1').then(function (results) {
            return results;
        });
    };

    // Gaunamas dalyvių sąrašas, pagal grupę
    var _getUserListGroup = function (compId, group) {
        return $http.get(serviceBase + 'api/competitionsclim/' + compId + '/1/' + group).then(function (results) {
            return results;
        });
    };

    // Gaunamas trasų sąrašas, pagal grupę
    var _getRouteListType = function (compId, type) {
        return $http.get(serviceBase + 'api/routeClim/' + compId + '/' + type).then(function (results) {
            return results;
        });
    };

    // Gaunamas trasų sąrašas be grupės
    var _getRouteList = function (compId) {
        return $http.get(serviceBase + 'api/routeClim/' + compId).then(function (results) {
            return results;
        });
    };

    // Pildyti teisėjo lapą
    var _updateJudgesPaper = function (routeId, userId, id) {
        var updaterequest = $http({
            method: 'put',
            url: serviceBase + 'api/judgespaperClim/' + routeId + '/' + userId + '/' + id,
        });

        return updaterequest;
    };

    // Gaunamas teisejo lapas
    var _getJudgePaper = function (routeId, userId) {
        return $http.get(serviceBase + 'api/judgespapersClim/' + routeId + '/' + userId + '/1').then(function (results) {
            return results;
        });
    };

    // KKT VARŽYBOMS NAUDOJAMI METODAI

    // Gaunamas trasų sąrašas, pagal grupę
    var _getRouteListTypeKKT = function (compId, type) {
        return $http.get(serviceBase + 'api/routeKKT/' + compId + '/' + type).then(function (results) {
            return results;
        });
    };

    // Gaunamas komandų sąrašas
    var _getTeamListGroup = function (compId, group) {
        return $http.get(serviceBase + 'api/competitionsKTT/' + compId + '/1/' + group).then(function (results) {
            return results;
        });
    };

    // Gaunamas baudu sarasas
    var _getPenaltyList = function () {
        return $http.get(serviceBase + 'api/penalty').then(function (results) {
            return results;
        });
    };
   
    // Gaunamas KKT varzybu teisjo lapas + jam priklausomu baudu sarasas
    var _getJudgesPaperKKT = function (routeId, teamId) {
        return $http.get(serviceBase + 'api/judgespapersKKT/' + routeId + '/' + teamId).then(function (results) {
            return results;
        });
    };

    // Redaguojamas KKT varzybu teisjo lapas
    var _updateJudgesPaperKKT = function (paperId, paper) {
        var updaterequest = $http({
            method: 'put',
            url: serviceBase + 'api/judgespapersKKT/' + paperId,
            data: paper
        });

        return updaterequest;
    };

    // Pridėti naują baudą
    var _addPenalty = function (paperId, penaltyId) {
        var addrequest = $http({
            method: 'post',
            url: serviceBase + 'api/penaltyquantity/' + paperId + '/' + penaltyId
        });

        return addrequest;
    };

    // Redaguoti baudą
    var _updatePenalty = function (penaltyId, id) {
        var updaterequest = $http({
            method: 'post',
            url: serviceBase + 'api/penaltyquantity/' + penaltyId + '/' + id
        });

        return updaterequest;
    };

    // Naikinti baudą
    var _deletePanalty = function (penaltyId) {
        var deleterequest = $http({
            method: 'delete',
            url: serviceBase + 'api/penaltyquantity/' + penaltyId
        });

        return deleterequest;
    };

    /** PRISKIRIMAI */
    judgeServiceFactory.getCompetitionList = _getCompetitionList;
    judgeServiceFactory.getUserList = _getUserList;
    judgeServiceFactory.updateJudgesPaper = _updateJudgesPaper;
    judgeServiceFactory.getRouteListType = _getRouteListType;
    judgeServiceFactory.getUserListGroup = _getUserListGroup;
    judgeServiceFactory.getJudgePaper = __getJudgePaper;
    judgeServiceFactory.getRouteList = _getRouteList;
    judgeServiceFactory.getTeamListGroup = _getTeamListGroup;
    judgeServiceFactory.getRouteListTypeKKT = _getRouteListTypeKKT;
    judgeServiceFactory.getPenaltyList = _getPenaltyList;
    judgeServiceFactory.getJudgesPaperKKT = _getJudgesPaperKKT;
    judgeServiceFactory.updateJudgesPaperKKT = _updateJudgesPaperKKT;
    judgeServiceFactory.addPenalty = _addPenalty;
    judgeServiceFactory.updatePenalty = _updatePenalty;
    judgeServiceFactory.deletePanalty = _deletePanalty;
    
    
    return judgeServiceFactory;

}]);