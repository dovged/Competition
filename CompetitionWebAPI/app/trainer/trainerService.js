'use strict';
app.factory('trainerService', ['$http', 'authService', function ($http, authService) {

    var serviceBase = 'http://localhost:52336/';
    var trainerServiceFactory = {};
    var user = authService.authentication.userName;

    return trainerServiceFactory;

}]);