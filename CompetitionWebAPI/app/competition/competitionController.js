'use strict';
app.controller('competitionController', ['$scope', '$location', 'competitionService', 'localStorageService', function ($scope, $location, competitionService, localStorageService) {

    $scope.competitionList = [];
    $scope.noCompetition = false;
    $scope.KKTOrg = false;

    loadCompetitionList();

    // užkraunamas varžybų sąrašas;
    function loadCompetitionList() {
        $scope.noCompetition = false;
        competitionService.getCompetitionList().then(function (results) {
            $scope.competitionList = results.data;
            localStorageService.remove("CompDetails");
            $scope.noCompetition = true;
        });

        competitionService.KKTOrg().then(function (results) {
            $scope.KKTOrg = true;
        });
    }

    /** Perėjimas į pasirinktų varžybų inforamcijos langą*/
    $scope.getCompInfo = function (Id) {
        localStorageService.set("CompDetails", Id);
        $location.path("/compInfo")
    }

}]);