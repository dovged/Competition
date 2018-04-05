'use strict';
app.factory('userService',['$http', 'authService', function ($http, authService) {

    var serviceBase = 'http://localhost:52336/';
    var userServiceFactory = {};
    var _user = authService.authentication.userName;

    // Grąžinti sąrašą
    var _getUserTeam = function () {

        return $http.get(serviceBase + "api/team/" + _user + "/1").then(function (results) {
            return results;
        });
    };

    userServiceFactory.getUserTeam = _getUserTeam;
   

    return userServiceFactory;

}]);