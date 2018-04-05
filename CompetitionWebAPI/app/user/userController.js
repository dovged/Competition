'use strict';
app.controller('userController', ['$scope', 'userService', function ($scope, userService) {

    $scope.team = [];
    loadUserInfo();

    // Užkraunami duomenys į lentelę
    function loadUserInfo() {
        userService.getUserTeam().then(function (results) {

            $scope.team = results.data;

        }, function (error) {
            //alert(error.data.message);
        });
    }


}]);