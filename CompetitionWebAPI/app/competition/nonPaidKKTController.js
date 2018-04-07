'use strict';
app.controller('nonPaidKKTController', ['$scope', '$location', 'competitionService', 'localStorageService', function ($scope, $location, competitionService, localStorageService) {

    $scope.competitorsList = [];
    $scope.CompId;
    loadCompetitorsList();

    // užkraunamas varžybų sąrašas;
    function loadCompetitorsList() {
        $scope.CompId = localStorageService.get("CompDetails")
        competitionService.getNonPaidKKT($scope.CompId).then(function (results) {
            $scope.competitorsList = results.data;
        }, function (error) {
            // alert(error.data.message);
        });
    }

    $scope.paidKKT = function (Id) {
        competitionService.paidKKT(Id).then(function (results) {
            loadCompetitorsList();
        }, function (error) {
            // alert(error.data.message);
        });
    }

}]);