'use strict';
app.controller('nonPaidKKTController', ['$scope', '$location', 'competitionService', 'localStorageService',
    function ($scope, $location, competitionService, localStorageService) {

    $scope.competitorsList = [];
    $scope.CompId;
    loadCompetitorsList();
    $scope.noCompetitors = false;

    // užkraunamas varžybų dalyvių komandų sąrašas;
    function loadCompetitorsList() {
        $scope.CompId = localStorageService.get("CompDetails")
        competitionService.getNonPaidKKT($scope.CompId).then(function (results) {
            $scope.competitorsList = results.data;
            $scope.noCompetitors = true;
        });
    };

    // Pažymima, kad dalyvaujanti komanda susimokėjo
    $scope.paidKKT = function (Id) {
        competitionService.paidKKT(Id).then(function (results) {
            loadCompetitorsList();
        });
    };

}]);