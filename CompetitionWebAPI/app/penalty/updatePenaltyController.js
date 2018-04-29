'use strict';
app.controller('updatePenaltyController', ['$scope', 'penaltyService', '$location', 'localStorageService', function ($scope, penaltyService, $location, localStorageService) {

    $scope.penalty = {
        Id: '',
        Name: '',
        Points: ''
    };

    loadPenaltyInfo();

    // užkrauna vienos baudos informaciją, pagal ID
    function loadPenaltyInfo() {
        // gaunamas baudos ID
        $scope.penalty.Id = localStorageService.get("penaltyId");

        penaltyService.get($scope.penalty.Id).then(function (results) {
            var p = results.data;
            $scope.penalty.Id = p.Id;
            $scope.penalty.Name = p.Name;
            $scope.penalty.Points = p.Points;
        });
    };

    // Atnaujinama baudos informacija
    $scope.update = function () {
        var p = {
            Id: $scope.penalty.Id,
            Name: $scope.penalty.Name,
            Points: $scope.penalty.Points
        };

        penaltyService.update($scope.penalty.Id, p).then(function (results) {
            // Grąžinana į baudų sąrašą
            $location.path("/penalties");
        });
    };

}]);