'use strict';
app.controller('trainerController', ['$scope', 'trainerService', 'localStorageService', '$location', function ($scope, trainerService, localStorageService, $location) {

    $scope.userList = {};
    $scope.teamList = {};
    $scope.noKids = false;
    $scope.KKTTrainer = false;
    loadInfo();

    /** Užkraunamas nepilnamečių dalyvių sąrašas, pagal trenerį*/
    function loadInfo() {
        trainerService.getUserList().then(function (results) {
            $scope.userList = results.data;
            $scope.noKids = true;
        });

        trainerService.KKTTrainer().then(function (results) {
            $scope.KKTTrainer = true;
        });

        localStorageService.remove("TraineeId");
        if ($scope.KKTTrainer) {
            trainerService.getTeams().then(function (results) {
                $scope.teamList = results.data;
            });
        }
    };

    /** Nukreipiama į dalyvio informacijos redagavimo langą*/
    $scope.updateTrainee = function (Id) {
        localStorageService.set("TraineeId", Id);
        $location.path("/updateTrainee");
    };

    // Išmesti narį iš komandos
    $scope.removeMember = function (id) {
        trainerService.removeMember(id).then(function (results) {
            loadInfo();
        });
    };

}]);