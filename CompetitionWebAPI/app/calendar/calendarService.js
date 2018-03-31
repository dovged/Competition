'use strict';
app.factory('calendarService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:52336/';
    var calendarServiceFactory = {};

    var _getCalendar = function () {
        return $http.get(serviceBase + 'api/competition').then(function (results) {
            return results;
        });
    };

    calendarServiceFactory.getCalendar = _getCalendar;

    return calendarServiceFactory;
}]);