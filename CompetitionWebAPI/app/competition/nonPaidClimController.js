'use strict';
app.controller('nonPaidClimController', ['$scope', '$location', 'competitionService', 'localStorageService', function ($scope, $location, competitionService, localStorageService) {

    $scope.competitorsList = [];
    $scope.CompId;
    loadCompetitorsList();

    // užkraunamas varžybų sąrašas;
    function loadCompetitorsList() {
        $scope.CompId = localStorageService.get("CompDetails")
        competitionService.getNonPaidClim($scope.CompId).then(function (results) {
            $scope.competitorsList = results.data;
        }, function (error) {
            // alert(error.data.message);
        });
    }


    $scope.paidClim = function (Id) {
        competitionService.paidClim(Id).then(function (results) {
            loadCompetitorsList();
        }, function (error) {
            // alert(error.data.message);
        });
    }

}]);