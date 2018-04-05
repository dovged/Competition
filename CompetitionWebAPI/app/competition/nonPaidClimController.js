'use strict';
app.controller('nonPaidClimController', ['$scope', '$location', 'competitionService', 'localStorageService', function ($scope, $location, competitionService, localStorageService) {

    $scope.competitorsList = [];
    $scope.CompId;
    loadCompetitionList();

    // užkraunamas varžybų sąrašas;
    function loadCompetitorsList() {
        $scope.CompId = localStorageService.get("CompDetails")
        competitionService.getNonPaidClim($scope.CompId).then(function (results) {
            $scope.competitorsList = results.data;
            localStorageService.remove("CompDetails");
        }, function (error) {
            // alert(error.data.message);
        });
    }

}]);