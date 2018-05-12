'use strict';
app.controller('nonPaidClimController', ['$scope', '$location', 'competitionService', 'localStorageService',
    function ($scope, $location, competitionService, localStorageService) {

    $scope.competitorsList = [];
    $scope.CompId;
    $scope.noCompetitors = false;
    loadCompetitorsList();

    // užkraunamas varžybų dalyvių sąrašas;
    function loadCompetitorsList() {
        $scope.CompId = localStorageService.get("CompDetails");
        $scope.competitorsList = [];
        $scope.noCompetitors = false;
        competitionService.getNonPaidClim($scope.CompId).then(function (results) {
            $scope.competitorsList = results.data;
            $scope.noCompetitors = true;
        });
    };

    // Pažymima, kad dalyvis susimokėjo
    $scope.paidClim = function (Id) {
        competitionService.paidClim(Id).then(function (results) {
            loadCompetitorsList();
        });
    };

}]);