'use strict';
app.controller('signupController', ['$scope', '$location', '$timeout', 'authService', function ($scope, $location, $timeout, authService) {

    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.registration = {
        userName: "",
        password: "",
        confirmPassword: ""
    };

    $scope.user = {
        Id: '',
        Name: '',
        LastName: '',
        TelNumber: '',
        Email: '',
        ClubId: '',
        TeamId: '',
        UserId: '',
        Active: '',
        TrainerId: '',
        BirthYear: '',
        Lytis: ''
    };

    /** Regsitracija į sistemos*/
    $scope.signUp = function () {

        authService.saveRegistration($scope.registration).then(function (response) {

            $scope.savedSuccessfully = true;
            $scope.message = "vartotojas sėkmingai užregistruotas, po 2 sekundžių matysite Prisijungimo langą.";
            var u = {
                Id: $scope.user.Id,
                Name: $scope.user.Name,
                LastName: $scope.user.LastName,
                TelNumber: $scope.user.TelNumber,
                Email: $scope.user.Email,
                ClubId: $scope.user.ClubId,
                TeamId: $scope.user.TeamId,
                UserId: $scope.user.UserId,
                Active: $scope.user.Active,
                TrainerId: $scope.user.TrainerId,
                BirthYear: $scope.user.BirthYear,
                Lytis: $scope.user.Lytis
            };

            authService.addUserInfo($scope.registration.userName, u).then(function (response) { });
            startTimer();

        },
            function (response) {
                var errors = [];
                for (var key in response.data.modelState) {
                    for (var i = 0; i < response.data.modelState[key].length; i++) {
                        errors.push(response.data.modelState[key][i]);
                    }
                }
                $scope.message = "Registracija nepavyko dėl:" + errors.join(' ');
            });
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    }

}]);