'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

    $scope.loginData = {
        userName: "",
        password: ""
    };

    $scope.message = "";
     /** Prisijungti prie sistemos*/
    $scope.login = function () {

        authService.login($scope.loginData).then(function (response) {

            $location.path('/home');

        },
            function (err) {
                $scope.message = err.error_description;
            });
    };

}]);