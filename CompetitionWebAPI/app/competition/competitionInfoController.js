'use strict';
app.controller('competitionInfoController', ['$scope', 'competitionService', function ($scope, competitionService) {

    $scope.competitionId;


    // užkraunamas varžybų sąrašas;


    competitionService.getCompetitionList().then(function (results) {
        $scope.competitionList = results.data;
    }, function (error) {
        // alert(error.data.message);
    });



}]);