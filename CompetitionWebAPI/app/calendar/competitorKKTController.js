'use strict';
app.controller('competitorKKTController', ['$scope', 'calendarService', '$location', 'localStorageService', function ($scope, calendarService, $location, localStorageService) {

    $scope.competitor = {
        CompetitionId: '',
        Group: '',
    };
    $scope.competition = {
        Name: '',
        Date: '',
        Club: ''
    };

    calendarService.getCompetitionDetails(localStorageService.get("calendarId")).then(function (results) {
        var comp = results.data;
        $scope.competition.Name = comp.Name;
        $scope.competition.Date = comp.Date2;
        $scope.competition.Club = comp.Club;
    });

    //Išsaugojama baudos informacija
    $scope.add = function () {
        var c = {
            CompetitionId: localStorageService.get("calendarId"),
            Group: $scope.competitor.Group,s
        };

        calendarService.addCompetitorKKT(c, c.CompetitionId).then(function (results) {
            $location.path("/calendar");
        });
    };

}]);