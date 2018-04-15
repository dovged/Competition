'use strict';
app.controller('competitionInfoController', ['$scope', 'competitionService', 'localStorageService', '$location', function ($scope, competitionService, localStorageService, $location) {

    $scope.competition = {
        Id: "",
        Name: "",
        Date: "",
        MainRouteCreatorId: "",
        MainRouteCreatorName: "",
        MainJudgeId: "",
        MainJudgeName: "",
        Type: "",
        Open: "",
        Update: "",
        ClimbType: ""
    };
    $scope.routeList = {};
    $scope.judgesList = {};
    $scope.noJudges = false;
    $scope.KKTRoutes = false;
    $scope.userList = {};
    $scope.newJudge = '';
    $scope.usersClubList = {};
    loadCompInfo();

    // užkraunamas varžybų sąrašas;
    function loadCompInfo() {
        $scope.competition.Id = localStorageService.get("CompDetails");     
        
        // Varžybų pagrindinė informacija
        competitionService.getCompetitionDetails($scope.competition.Id).then(function (results) {
            var c = results.data;
            $scope.competition.Id = c.Id;
            $scope.competition.Name = c.Name;
            $scope.competition.Date = c.Date;
            $scope.competition.MainRouteCreatorId = c.MainRouteCreatorId;
            $scope.competition.MainRouteCreatorName = c.MainRouteCreatorName;
            $scope.competition.MainJudgeId = c.MainJudgeId;
            $scope.competition.MainJudgeName = c.MainJudgeName;
            $scope.competition.Type = c.Type;
            $scope.competition.Open = c.Open;
            $scope.competition.Update = c.Update;
            $scope.competition.ClimbType = c.ClimbType;

        });

        competitionService.getJudges($scope.competition.Id).then(function (results) {
            $scope.judgesList = results.data;
            $scope.noJudges = true;
        });

        competitionService.getUsers().then(function (results) {
            $scope.userList = results.data;
        });

        competitionService.getUsersClub().then(function (results) {
            $scope.usersClubList = results.data;
        });

        if (!$scope.competition.Type) {
            // KKT trasų informacija
            competitionService.getKKTRoutes($scope.competition.Id).then(function (results) {
                $scope.routeList = results.data;
                $scope.KKTRoutes = true;
            });
            localStorageService.remove("routeId");
        }
        
    };

    // Panaikinamas tesiėjas
    $scope.deleteJudge = function (id) {
        competitionService.deleteJudge(id).then(function (results) {
            loadCompInfo();
        });
    };

    // Pridedamas teisėjas
    $scope.addJudge = function () {
        competitionService.addJudge($scope.competition.Id, $scope.newJudge).then(function (results) {
            loadCompInfo();
        });
    };

    // Nukreipiamas vartotojas į atnaujimą KKT trasos informacijos
    $scope.updateKKTRoute = function (Id) {
        localStorageService.set("routeId", Id);
        $location.path("/updateKKTRoute");
    };

    // Atnaujinama varžybų pagrindinė informacija
    $scope.updateCompInfo = function () {
        var c = {
            Id: $scope.competition.Id,
            Name: $scope.competition.Name,
            Date: $scope.competition.Date,
            OrgId: $scope.competition.OrgId,
            MainJudgeId: $scope.MainJudgeId,
            MainRouteCreatorId: $scope.MainRouteCreatorId,
            Type: $scope.competition.Type,
            Open: $scope.competition.Open,
            Update: $scope.competition.Update,
            ClimbType: $scope.competition.ClimbType
        };

        competitionService.updateCompInfo($scope.competition.Id, c).then(function (results) {
            loadCompInfo();
        });

    }

}]);