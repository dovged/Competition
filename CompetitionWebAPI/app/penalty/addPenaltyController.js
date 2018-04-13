'use strict';
app.controller('addPenaltyController', ['$scope', 'penaltyService', '$location', function ($scope, penaltyService, $location) {

    $scope.pen = {
        Id: '',
        Name: '',
        Points: ''
    };

    //Išsaugojama baudos informacija
    $scope.add = function () {
        var p = {
            Name: $scope.pen.Name,
            Points: $scope.pen.Points
        };

        penaltyService.add(p).then(function (results) {
            $location.path("/penalties");
        }, function (error) {
            // alert(error.data.message);
        });
    };

}]);