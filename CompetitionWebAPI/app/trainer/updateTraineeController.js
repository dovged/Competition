'use strict';
app.controller('updateTraineeController', ['$scope', 'trainerService', '$location', 'localStorageService', function ($scope, trainerService, $location, localStorageService) {

    $scope.user = {
        Id: '',
        Name: '',
        LastName: '',
        BirthYear: '',
        Lytis: ''
    };

    getTraineeInfo();

    // Gaunama jaunojo sportininko informacija
    function getTraineeInfo() {
        $scope.user.Id = localStorageService.get("TraineeId");
        trainerService.getTraineeInfo($scope.user.Id).then(function (results) {
            var u = results.data;
            $scope.user.Name = u.Name;
            $scope.user.LastName = u.LastName;
            $scope.user.BirthYear = u.BirthYear2;
            $scope.user.Lytis = u.Lytis;
        });
    };

    //Išsaugojama vartotojo informacija
    $scope.updateTrainee = function () {
        var u = {
            Id: $scope.user.Id,
            Name: $scope.user.Name,
            LastName: $scope.user.LastName,
            BirthYear: $scope.user.BirthYear,
            Lytis: $scope.user.Lytis
        };

        trainerService.updateTrainee(u, $scope.user.Id).then(function (results) {
            $location.path("/trainer");
        });
    };

}]);