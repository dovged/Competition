'use strict';
app.controller('judgesPapersKKTController', ['$scope', 'judgeService', 'localStorageService', '$location', function ($scope, judgeService, localStorageService, $location) {

    $scope.competition = {
        Id: ''
    };
    $scope.routeList = {};
    $scope.routeType = {
        Id: ''
    };
    $scope.teamList = {};
    $scope.paper = {
        Id: '',
        Time: '',
        Comment: '',
        TeamId: '',
        RouteId: '',
        Penalties : []
    };
    $scope.papersDetails = {
        routeId: '',
        teamId: ''
    };
    $scope.penaltyList = {};
    $scope.newPenalty = {
        Id: ''
    };
    $scope.showTeam = false;
    $scope.showPaper = false;

    // užkraunamas varžybų sąrašas;
    $scope.loadJudgesPapersInfo = function () {
        $scope.competition.Id = localStorageService.get("CompjudgeId");

        /**Užkraunama dalyvių sąrašas*/
        judgeService.getTeamListGroup($scope.competition.Id, $scope.routeType.Id).then(function (results) {
            $scope.teamList = results.data;
        });

        /**Užkraunamos trasos*/
        judgeService.getRouteListTypeKKT($scope.competition.Id, $scope.routeType.Id).then(function (results) {
            $scope.routeList = results.data;
        });

        /** Užkraunamos baudų sąrašas*/
        judgeService.getPenaltyList().then(function (results) {
            $scope.penaltyList = results.data;
        });

        $scope.showTeam = true;

    };

    // Gaunamas teisėjo lapas pagal komandą ir trasą
    $scope.getJudgePaper = function () {
        $scope.showPaper = true;

        judgeService.getJudgesPaperKKT($scope.papersDetails.routeId, $scope.papersDetails.userId).then(function (results) {
            var p = results.data;
            $scope.paper.Id = p.Id;
            $scope.paper.Time = p.Time;
            $scope.paper.Comment = p.Comment;
            $scope.paper.TeamId = p.TeamId;
            $scope.paper.RouteId = p.RouteId;
            $scope.paper.Penalties = p.Penalties;
        });
    };

    // Atnaujinamas teisejo lapas : pridedama prie topu bandymu 1
    $scope.updatePaperAdd = function (Id) {
        judgeService.updatePenalty(Id, 1).then(function (results) {
            $scope.getJudgePaper();
        });
    };

    // Atnaujinamas teisejo lapas : sumazinama topu bandymai 1
    $scope.updatePaperMinus = function (Id) {
        judgeService.updatePenalty(Id, 2).then(function (results) {
            $scope.getJudgePaper();
        });
    };

    // Pirdedama nauja bauda
    $scope.addPenalty = function () {
        judgeService.addPenalty($scope.paperDetails.Id, $scope.newPenalty.Id).then(function (results) {
            $scope.getJudgePaper();
        });
    };

    // Naikinti buada
    $scope.deletePenalty = function (Id) {
        judgeService.deletePanalty(Id).then(function (results) {
            $scope.getJudgePaper();
        });
    };

    // Pirdedama nauja bauda
    $scope.updateJudgesPaper = function () {
        var p = {
            Id: $scope.paper.Id,
            Time: $scope.paper.Time,
            Comment: $scope.paper.Comment,
            TeamId: $scope.paper.TeamId,
            RouteId: $scope.paper.RouteId

        };
        judgeService.updateJudgesPaperKKT($scope.paperDetails.Id, p).then(function (results) {
            $scope.getJudgePaper();
        });
    };

}]);