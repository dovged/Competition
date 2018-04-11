'use strict';
app.controller('competitionController', ['$scope', '$location', 'competitionService', 'localStorageService', function ($scope, $location, competitionService, localStorageService) {

    $scope.competitionList = [];
    loadCompetitionList();

    // užkraunamas varžybų sąrašas;
    function loadCompetitionList() {
        competitionService.getCompetitionList().then(function (results) {
            $scope.competitionList = results.data;
            localStorageService.remove("CompDetails");
        }, function (error) {
           // alert(error.data.message);
        });
    }

    /** Perėjimas į pasirinktų varžybų inforamcijos langą*/
    $scope.getCompInfo = function (Id) {
        localStorageService.set("CompDetails", Id);
        $location.path("/compInfo")
    }

}]);