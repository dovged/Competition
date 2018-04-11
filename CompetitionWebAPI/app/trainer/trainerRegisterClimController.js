'use strict';
app.controller('trainerRegisterClimController', ['$scope', 'trainerService', function ($scope, trainerService) {

    $scope.climberList = {};
    $scope.competitionId = '';
    $scope.compInfo = {};

    loadUserList();

    /** Grąžinamas sąrašas dalyvių */
    function loadUserList() {
        $scope.competitionId = localStorageService.get("CompTrainerId");
        trainerService.getRegisterClim($scope.competitionId).then(function (results) {
            $scope.climberList = results.data;
        });

        trainerService.getCompInfo($scope.competitionId).then(function (results) {
            $scope.compInfo = results.data;
        });
    };

    // Regsitruojamas dalyvis į varžybas
    $scope.register = function (Id) {
        trainerService.addRegisterClim($scope.competitionId, Id).then(function (results) {
            loadUserList();
        });
    };

    // Išregistruojamas iš varžybų dalyvis
    $scope.remove = function (Id) {
        trainerService.removeRegisterClim($scope.competitionId, Id).then(function (results) {
            loadUserList();
        });
    };

}]);