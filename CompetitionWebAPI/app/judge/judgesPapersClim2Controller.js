'use strict';
app.controller('judgesPapersClim2Controller', ['$scope', 'judgeService', 'localStorageService', '$location', function ($scope, judgeService, localStorageService, $location) {
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
        RouteId
    };
    $scope.papersDetails = {
        routeId: '',
        userId: ''
    };
    $scope.showUser = false;
    $scope.showPaper = false;
    $scope.flash = false;
    $scope.rredPoint = false;

    // užkraunamas varžybų sąrašas;
    $scope.loadJudgesPapersInfo = function () {
        $scope.competition.Id = localStorageService.get("CompjudgeId");

        /**Užkraunama dalyvių sąrašas*/
        judgeService.getUserListGroup($scope.competition.Id, $scope.routeType.Id).then(function (results) {
            $scope.userList = results.data;
        });

        /**Užkraunamos trasos*/
        judgeService.getRouteList($scope.competition.Id).then(function (results) {
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
            $scope.paper.RouteId = p.RouteId;
            if ($scope.paper.TopAttempt == 1) {
                $scope.flash = true;
                $scope.redPoint = false;
            }
            else if ($scope.paper.TopAttempt == 2) {
                $scope.flash = false;
                $scope.redPoint = true;
            }
        });
    };

    // Atnaujinamas teisejo lapas : topas 1 bandymu
    $scope.updatePaperFlash = function () {
        judgeService.updateJudgesPaper($scope.paperDetails.routeId, $scope.paperDetails.userId, 1).then(function (results) {
            $scope.getJudgePaper();
        });
    };

    // Atnaujinamas teisejo lapas : topas bet kuriuo badnymu
    $scope.updatePaperRedPoint = function () {
        judgeService.updateJudgesPaper($scope.paperDetails.routeId, $scope.paperDetails.userId, 2).then(function (results) {
            $scope.getJudgePaper();
        });
    };

    // Atnaujinamas teisejo lapas : bonusas
    $scope.updatePaperBonus = function () {
        judgeService.updateJudgesPaper($scope.paperDetails.routeId, $scope.paperDetails.userId, 3).then(function (results) {
            $scope.getJudgePaper();
        });
    };

}]);