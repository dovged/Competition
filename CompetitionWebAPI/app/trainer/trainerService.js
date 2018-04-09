'use strict';
app.factory('trainerService', ['$http', 'authService', function ($http, authService) {

    var serviceBase = 'http://localhost:52336/';
    var trainerServiceFactory = {};
    var _user = authService.authentication.userName;

    var _getUserList = function () {
        return $http.get(serviceBase + 'api/user/' + _user + '/1').then(function (results) {
            return results;
        });
    };

    // Treneris užregistruoja naują dalyvį
    var _addTrainee = function (t) {
        var addrequest = $http({
            method: 'post',
            url: serviceBase + "api/userTrainee/" + _user,
            data: t
        });
    };

    trainerServiceFactory.getUserList = _getUserList;
    trainerServiceFactory.addTrainee = _addTrainee;

    return trainerServiceFactory;

}]);