'use strict';
app.controller('calendarController', ['$scope', 'calendarService', 'localStorageService', '$location', function ($scope, calendarService, localStorageService, $location) {

    $scope.competitionList = {};
    $scope.noCompetition = false;

    // užkraunamas varžybų sąrašas;
    calendarService.getCompetitionList().then(function (results) {
        $scope.competitionList = results.data;
        $scope.noCompetition = true;
    });


    /** Užsiregistruoti į varžybas*/
    $scope.registerComp = function (Id, Type) {
        localStorageService.set("calendarId", Id);
        if (Type) {
            $location.path("/competitorClim");
        }
        else {
            $location.path("/competitorKKT");
        }
    };

}]);