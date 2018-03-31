'use strict';
app.controller('calendarController', ['$http', 'calendarService', function ($scope, calendarService) {

    $scope.calendar = [];

    calendarService.getCalendar().then(function (results) {

        $scope.calendar = results.data;
    }, function (error) {
        // aler(error.data.message);
        });

}]);