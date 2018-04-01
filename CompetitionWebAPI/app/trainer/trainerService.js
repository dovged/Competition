'use strict';
app.factory('trainerService', ['$http', 'authService', function ($http, authService) {

    var serviceBase = 'http://localhost:52336/';
    var trainerServiceFactory = {};
    var user = authService.authentication.userName;

    var _getUserList = function () {
        return $http.get(serviceBase + 'api/user/' + user + '/1').then(function (results) {
            return results;
        });
    };

    trainerServiceFactory.getUserList = _getUserList;

    return trainerServiceFactory;

}]);