'use strict';
app.controller('resultController', ['$scope', 'calendarService', '$location', 'localStorageService', function ($scope, calendarService, $location, localStorageService) {

    $scope.resultsList = {};
    $scope.competition = {
        Name: '',
        Date: '',
        Club: '',
        Type: '',
        ResultType: ''
    };
    $scope.KKTResults = false;
    $scope.KaboriaiResults = false;
    $scope.LBCResults = false;
    $sopce.LJBTResults = false;
    $scope.getGroupKKT = false;
    $scope.getGroupClim = false;

    $scope.compId = localStorageService.get("calendarId");


}]);