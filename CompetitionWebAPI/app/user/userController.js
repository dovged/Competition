'use strict';
app.controller('userController', ['$scope', 'userService', function ($scope, userService) {

    $scope.team = [];
    $scope.compClim = [];
    $scope.compKKT = [];
    loadUserInfo();

    // Užkraunami duomenys į lentelę
    function loadUserInfo() {
        userService.getUserTeam().then(function (results) {
            $scope.team = results.data;
        });

        userService.getCompListClim().then(function (results) {
            $scope.compClim = results.data;
        });

        userService.getCompListKKT().then(function (results) {
            $scope.compKKT = results.data;
        });
    }

    // Panaikinama varžybų registracija LAIPIOJIMAS
    $scope.deleteCompClim = function (Id) {
        userService.deleteCompClim(Id).then(function (results) {
            loadUserInfo();
        });
    };

    // Panaikinama varžybų registracija KKT
    $scope.deleteCompKKT = function (Id) {
        userService.deleteCompKKT(Id).then(function (results) {
            loadUserInfo();
        });
    };





}]);