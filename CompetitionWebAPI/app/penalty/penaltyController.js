'use strict';
app.controller('penaltyController', ['$scope', 'penaltyService', 'localStorageService', '$location', function ($scope, penaltyService, localStorageService, $location) {

    $scope.penaltyList = [];
    $scope.noPenalty = false;

    loadPenaltyList();

    // Užkraunami duomenys į lentelę
    function loadPenaltyList() {
        penaltyService.getPenaltyList().then(function (results) {
            $scope.penaltyList = results.data;
            $scope.noPenalty = true;
        });
        localStorageService.remove("penaltyId");
    };

    // Nukreipiama į baudos informacijos redagavimo langą
    $scope.update = function (Id) {
        localStorageService.set("penaltyId", Id);
        $location.path("/updatePenalty");
    };

    // Padaryti baudą neaktyvią
    $scope.delete = function (Id) {
        penaltyService.delete(Id).then(function (results) {
            loadPenaltyList();
        });
    }

   
   

}]);