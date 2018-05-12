'use strict';
app.controller('judgesPapersClim1Controller', ['$scope', 'judgeService', 'localStorageService', '$location', function ($scope, judgeService, localStorageService, $location) {
    $scope.competition = {
        Id: ''
    };
    $scope.routeList = {};
    $scope.routeType = {
        Id: ''
    };
    $scope.userList = {};
    $scope.paper = {
        Id: '',
        TopAttempt: '',
        BonusAttempt: '',
        ClimberId: '',
        RouteId: ''
    };
    $scope.papersDetails = {
        routeId: '',
        userId: ''
    };
    $scope.showUser = false;
    $scope.showPaper = false;


    // užkraunamas varžybų sąrašas;
    $scope.loadJudgesPapersInfo = function() {
        $scope.competition.Id = localStorageService.get("CompjudgeId");

         /**Užkraunama dalyvių sąrašas*/
        judgeService.getUserListGroup().then(function (results) {
            $scope.userList = results.data;
        });

        /**Užkraunamos trasos*/
        judgeService.getRouteListType($scope.competition.Id, $scope.routeType.Id).then(function (results) {
            $scope.routeList = results.data;
        });

        $scope.showUser = true;
        
    };

    // Gaunamas teisėjo lapas pagal dalyvį ir trasą
    $scope.getJudgePaper = function () {
        $scope.showPaper = true;

        judgeService.getJudgePaper($scope.papersDetails.routeId, $scope.papersDetails.userId).then(function (results) {
            var p = results.data;
            $scope.paper.Id = p.Id;
            $scope.paper.TopAttempt = p.TopAttempt;
            $scope.paper.BonusAttempt = p.BonusAttempt;
            $scope.paper.ClimberId = p.ClimberId;
            $scope.paper.routeId = p.RouteId;
        });
    };

    // Atnaujinamas teisejo lapas : pridedama prie topu bandymu 1
    $scope.updatePaperAddTop = function () {
        judgeService.updateJudgesPaper($scope.paperDetails.routeId, $scope.paperDetails.userId, 4).then(function (results) {
            $scope.getJudgePaper();
        });
    };

     // Atnaujinamas teisejo lapas : sumazinama topu bandymai 1
    $scope.updatePaperMinusTop = function () {
        judgeService.updateJudgesPaper($scope.paperDetails.routeId, $scope.paperDetails.userId, 5).then(function (results) {
            $scope.getJudgePaper();
        });
    };

    // Atnaujinamas teisejo lapas : pridedama prie bonusu bandymu 1
    $scope.updatePaperAddBonus = function () {
        judgeService.updateJudgesPaper($scope.paperDetails.routeId, $scope.paperDetails.userId, 6).then(function (results) {
            $scope.getJudgePaper();
        });
    };

    // Atnaujinamas teisejo lapas : sumazinama bonusu bandymai 1
    $scope.updatePaperMinusBonus = function () {
        judgeService.updateJudgesPaper($scope.paperDetails.routeId, $scope.paperDetails.userId, 7).then(function (results) {
            $scope.getJudgePaper();
        });
    };

}]);