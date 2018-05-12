'use strict';
app.controller('trainerController', ['$scope', 'trainerService', 'localStorageService', '$location', function ($scope, trainerService, localStorageService, $location) {

    $scope.userList = {};
    $scope.teamList = {};
    $scope.team = {
        Name: '',
        TeamCaptainId: ''
    };
    $scope.newMemberTeam = {
        MemberId: '',
        TeamId: ''
    };

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

    // Prideti komanda
    $scope.addTeam = function () {
        var t = {
            Name: $scope.team.Name,
            TeamCaptainId: "0"
        }
        trainerService.addTeam(t).then(function (results) {
            loadUserInfo();
        });
    };

    // Prideti dalyvi i komanda
    $scope.addMember = function () {
        userService.addMember($scope.newMemberTeam.MemberId, $scope.newMemberTeam.TeamId).then(function (results) {
            loadInfo();
        });
    };
}]);