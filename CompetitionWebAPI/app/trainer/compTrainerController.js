'use strict';
app.controller('compTrainerController', ['$scope', 'trainerService', function ($scope, trainerService) {

    $scope.competitionList = [];

    // užkraunamas varžybų sąrašas;

    compTrainerService.getCompetitionList().then(function (results) {
        $scope.competitionList = results.data;
    }, function (error) {
        // alert(error.data.message);
    });



}]);