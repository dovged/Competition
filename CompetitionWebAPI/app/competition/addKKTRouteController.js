'use strict';
app.controller('addKKTRouteController', ['$scope', 'competitionService', '$location', 'localStorageService', function ($scope, competitionService, $location, localStorageService) {

    $scope.route = {
        Id: '',
        Name: '',
        Time: '',
        Points: '',
        CompId: '',
        Type: ''
    };

    // Išsaugojama baudos informacija
    $scope.add = function () {
        var r = {
            Name: $scope.route.Name,
            Time: $scope.route.Time,
            Points: $scope.route.Points,
            CompetitionId: localStorageService.get("CompDetails"),
            Type: $scope.route.Type
        };

        competitionService.addKKTRoute(r).then(function (results) {
            $location.path("/compInfo");
        });
    };

}]);