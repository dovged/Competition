﻿'use strict';
app.controller('calendarController', ['$scope', 'calendarService', function ($scope, calendarService) {

    $scope.competitionList = [];

    // užkraunamas varžybų sąrašas;

    calendarService.getCompetitionList().then(function (results) {
        $scope.competitionList = results.data;
    }, function (error) {
        // alert(error.data.message);
    });



}]);