'use strict';
app.controller('updateKKTRouteController', ['$scope', 'competitionService', '$location', 'localStorageService', function ($scope, competitionService, $location, localStorageService) {

    $scope.route = {
        Id: '',
        Name: '',
        Time: '',
        Points: '',
        CompId: '',
        Type: ''
    };

    loadRouteInfo();

    // užkrauna trasos informacija
    function loadRouteInfo() {
        $scope.route.Id = localStorageService.get("routeId");
        $scope.route.CompId = localStorageService.get("CompDetails");
        competitionService.getKKTRouteInfo($scope.route.Id).then(function (results) {
            var r = results.data;
            $scope.route.Name = r.Name;
            $scope.route.Points = r.Points;
            $scope.route.Time = r.Time;
            $scope.route.Type = r.Type;
        });
    };

    // Išsaugojama baudos informacija
    $scope.update = function () {
        var r = {
            Id: $scope.route.Id,
            Name: $scope.route.Name,
            Time: $scope.route.Time,
            Points: $scope.route.Points,
            CompetitionId: $scope.route.CompId,
            Type: $scope.route.Type
        };

        competitionService.updateKKTRoute($scope.route.Id, r).then(function (results) {
            $location.path("/compInfo");
        });
    };

}]);