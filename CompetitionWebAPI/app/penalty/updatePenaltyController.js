'use strict';
app.controller('updatePenaltyController', ['$scope', 'penaltyService', '$location', 'localStorageService', function ($scope, penaltyService, $location, localStorageService) {

    $scope.penalty = {
        Id: '',
        Name: '',
        Points: ''
    };

    loadPenaltyInfo();

    // užkrauna baudos informacija
    function loadPenaltyInfo() {
        $scope.penalty.Id = localStorageService.get("penaltyId");

        penaltyService.get($scope.penalty.Id).then(function (results) {
            var p = results.data;
            $scope.penalty.Id = p.Id;
            $scope.penalty.Name = p.Name;
            $scope.penalty.Points = p.Points;
        });
    };

    //Išsaugojama baudos informacija
    $scope.update = function () {
        var p = {
            Id: $scope.penalty.Id,
            Name: $scope.penalty.Name,
            Points: $scope.penalty.Points
        };

        penaltyService.update($scope.penalty.Id, p).then(function (results) {
            $location.path("/penalties");
        }, function (error) {
            // alert(error.data.message);
        });
    };

}]);