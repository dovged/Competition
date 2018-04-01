'use strict';
app.controller('trainerController', ['$scope', 'trainerService', function ($scope, trainerService) {

    $scope.userList = [];
    loadUserList();

    function loadUserList() {
        trainerService.getUserList().then(function (results) {
            $scope.userList = results.data;
        });
    }
    

}]);