'use strict';
app.controller('addCompetitionController', ['$scope', 'competitionService', '$location', function ($scope, competitionService, $location) {

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

    $scope.userClubList = {};

    competitionService.getUsersClub().then(function (results) {
        $scope.userClubList = results.data;
    });

    // Užregitraujamos naujos varžybos
    $scope.add = function () {
        var c = {
            Id: $scope.competition.Id,
            Name: $scope.competition.Name,
            Date: $scope.competition.Date,
            MainJudgeId: $scope.competition.MainJudgeId,
            MainRouteCreatorId: $scope.competition.MainRouteCreatorId,
            Type: $scope.competition.Type,
            ClimbType: $scope.competition.ClimbType
        };

        competitionService.addCompetition(c).then(function (results) {
            $location.path("/competitionOrg");
        });

    }

}]);