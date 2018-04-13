'use strict';
app.controller('addTraineeController', ['$scope', 'trainerService', '$location', function ($scope, trainerService, $location) {

    $scope.user = {
        Id: '',
        Name: '',
        LastName: '',
        BirthYear: '',
        Lytis: ''
    };

    //Išsaugojama vartotojo informacija
    $scope.addTrainee = function () {
        var u = {
            Id: $scope.user.Id,
            Name: $scope.user.Name,
            LastName: $scope.user.LastName,
            BirthYear: $scope.user.BirthYear,
            Lytis: $scope.user.Lytis
        };

        trainerService.addTrainee(u).then(function (results) {
            $location.path("/trainer");
        });
    };

}]);