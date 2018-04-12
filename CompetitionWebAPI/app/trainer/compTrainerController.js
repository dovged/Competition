'use strict';
app.controller('compTrainerController', ['$scope', 'trainerService', '$location', 'localStorageService', function ($scope, trainerService, $location, localStorageService) {

    $scope.competitionList = [];
    $scope.NoComp = false;

    // užkraunamas varžybų sąrašas;

    trainerService.getCompetitionListTrainer().then(function (results) {
        $scope.competitionList = results.data;
        $scope.NoComp = true;
        localStorageService.remove("CompTrainerId");
    }, function (error) {
        // alert(error.data.message);
        });

    /** Perėjimas į registracijos langą pasirinktoms varžyboms*/
    $scope.register = function (Id, type) {
        localStorageService.set("CompTrainerId", Id);
        // LAIPIOJIMO VARŽYBŲ ATVEJU
        if (type) {
            $location.path("/registerCompClim");
        }
        // KKT VARŽYBŲ ATVEJU
        else {
            $location.path("registerCompKKT");
        }
    };




}]);