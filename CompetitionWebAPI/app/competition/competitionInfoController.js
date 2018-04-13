'use strict';
app.controller('competitionInfoController', ['$scope', 'competitionService', 'localStorageService', function ($scope, competitionService, localStorageService) {

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
    $scope.KKTorg = false;
    $scope.KKTRoutes = false;
    loadCompInfo();

    // užkraunamas varžybų sąrašas;
    function loadCompInfo() {
        $scope.Id = localStorageService.get("CompDetails");

        competitionService.KKTOrg().then(function (results) {
            $scope.KKTorg = true;
        });      
        
        // Varžybų pagrindinė informacija
        competitionService.getCompetitionDetails($scope.Id).then(function (results) {
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

        if ($scope.KKTorg) {
            // KKT trasų informacija
            competitionService.getKKTRoutes($scope.Id).then(function (results) {
                $scope.routeList = results.data;
                $scope.KKTRoutes = true;
            });  
        }
              
    };

}]);