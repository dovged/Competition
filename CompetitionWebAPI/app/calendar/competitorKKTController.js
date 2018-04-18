'use strict';
app.controller('competitorKKTController', ['$scope', 'calendarService', '$location', 'localStorageService', function ($scope, calendarService, $location, localStorageService) {

    $scope.competitor = {
        CompetitionId: '',
        TeamId: '',
        Group: '',
        Paid: ''

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
            TeamId: 0,
            Group: $scope.competitor.Group,
            Paid: "False"
        };

        calendarService.addCompetitorKKT(c, $scope.competitor.CompetitionId).then(function (results) {
            $location.path("/calendar");
        });
    };

}]);