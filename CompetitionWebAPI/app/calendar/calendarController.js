'use strict';
app.controller('calendarController', ['$scope', 'calendarService', function ($scope, calendarService) {

    $scope.competitionList = {};
    $scope.noCompetition = false;

    // užkraunamas varžybų sąrašas;
    calendarService.getCompetitionList().then(function (results) {
        $scope.competitionList = results.data;
        $scope.noCompetition = true;
    });

}]);