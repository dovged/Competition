'use strict';
app.controller('resultController', ['$scope', 'calendarService', '$location', 'localStorageService', function ($scope, calendarService, $location, localStorageService) {

    $scope.resultsList = {};
    $scope.resultsList2 = {};
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
    $scope.group = {
        Id: ''
    };

    $scope.compId = localStorageService.get("resultId");

    loadResults();

    function loadResults () {
        calendarService.getCompetitionDetails($scope.compId).then(function (results) {
            var comp = results.data;
            $scope.competition.Name = comp.Name;
            $scope.competition.Date = comp.Date2;
            $scope.competition.Club = comp.Club;
            $scope.competition.Type = comp.Type;
            $scope.competition.ResultType = comp.ClimbType;
        });

        if ($scope.competition.Type) {
            if ($scope.competition.ResultType == 1) {
                $scope.KaboriaiResults = true;
                calendarService.getResults($scope.compId, 1, "Vyras", "0").then(function (results) {
                    $scope.resultsList = results.data;
                });
                calendarService.getResults($scope.compId, 1, "Moteris", "0").then(function (results) {
                    $scope.resultsList2 = results.data;
                });

            }
            else if ($scope.competition.ResultType == 2) {
                $scope.KaboriaiLBC = true;
                calendarService.getResults($scope.compId, 1, "Vyras", "0").then(function (results) {
                    $scope.resultsList = results.data;
                });
                calendarService.getResults($scope.compId, 1, "Moteris", "0").then(function (results) {
                    $scope.resultsList2 = results.data;
                });
            }
            else if ($scope.competition.ResultType == 3) {
                $scope.getGroupClim = true;
            }
        }
        else {
            $scope.getGroupKKT = true;
        }
    };

    /** Randami rezultatai pagal grupe - LJBT varzyboms*/
    $scope.loadResulstLJBT = function () {
        $sopce.LJBTResults = true;
        calendarService.getResults($scope.compId, 3, "Vyras", $scope.group.Id).then(function (results) {
            $scope.resultsList = results.data;
        });
        calendarService.getResults($scope.compId, 3, "Moteris", $scope.group.Id).then(function (results) {
            $scope.resultsList2 = results.data;
        });
    };

    /** Randami rezultatai pagal grupe - KKT varzyboms*/
    $scope.loadResulstLKKT = function () {
        $scope.KKTResults = true;
        calendarService.getResults($scope.compId, $scope.competition.ClimbType, "0", $scope.group.Id).then(function (results) {
            $scope.resultsList = results.data;
        });
    };

    
}]);