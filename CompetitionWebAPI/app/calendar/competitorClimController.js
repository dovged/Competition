'use strict';
app.controller('competitorClimController', ['$scope', 'calendarService', '$location', 'localStorageService', function ($scope, calendarService, $location, localStorageService) {

    $scope.competitor = {
        CompetitionId: '',
        Group: ''
    };
    $scope.competition = {
        Name: '',
        Date: '',
        Club: ''
    };

    $scope.compId = localStorageService.get("calendarId");

    calendarService.getCompetitionDetails($scope.compId).then(function (results) {
        var comp = results.data;
        $scope.competition.Name = comp.Name;
        $scope.competition.Date = comp.Date2;
        $scope.competition.Club = comp.Club;
    });

    //Išsaugojama baudos informacija
    $scope.add = function () {
        var c = {
            CompetitionId: $scope.compId,
            Group: $scope.competitor.Group
        };

        calendarService.addCompetitorClim(c, $scope.compId).then(function (results) {
            $location.path("/calendar");
        });
    };

}]);