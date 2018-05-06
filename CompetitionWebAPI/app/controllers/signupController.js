'use strict';
app.controller('signupController', ['$scope', '$location', '$timeout', 'authService', 'userService',function ($scope, $location, $timeout, authService, userService) {

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
        BirthYear: '',
        Lytis: ''
    };

    $scope.clubs = {};

    /** Klubų sąrašas, atnaujinui skirta*/
    userService.getClubs().then(function (results) {
        $scope.clubs = results.data;
    });

    /** Regsitracija į sistemos*/
    $scope.signUp = function () {

        authService.saveRegistration($scope.registration).then(function (response) {
           
            var u = {
                Name: $scope.user.Name,
                LastName: $scope.user.LastName,
                TelNumber: $scope.user.TelNumber,
                Email: $scope.user.Email,
                ClubId: $scope.user.ClubId,
                BirthYear: $scope.user.BirthYear,
                Lytis: $scope.user.Lytis
            };

            authService.addUserInfo($scope.registration.userName, u).then(function (response) { });

            $scope.savedSuccessfully = true;
            $scope.message = "vartotojas sėkmingai užregistruotas, po 2 sekundžių matysite Prisijungimo langą.";
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