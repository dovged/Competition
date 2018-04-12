'use strict';
app.controller('trainerRegisterKKTController', ['$scope', 'trainerService', 'localStorageService', function ($scope, trainerService, localStorageService) {

    $scope.climberList = {};
    $scope.competitionId = '';
    $scope.compInfo = {};

    loadUserList();

    /** Grąžinamas sąrašas dalyvių */
    function loadUserList() {
        $scope.competitionId = localStorageService.get("CompTrainerId");
        trainerService.getRegisterKKT($scope.competitionId).then(function (results) {
            $scope.climberList = results.data;
        });

        trainerService.getCompInfo($scope.competitionId).then(function (results) {
            $scope.compInfo = results.data;
        });
    };

    // Regsitruojamas dalyvis į varžybas
    $scope.register = function (Id) {
        trainerService.addRegisterKKT($scope.competitionId, Id).then(function (results) {
            loadUserList();
        });
    };

    // Išregistruojamas iš varžybų dalyvis
    $scope.remove = function (Id) {
        trainerService.removeRegisterKKT($scope.competitionId, Id).then(function (results) {
            loadUserList();
        });
    };

}]);