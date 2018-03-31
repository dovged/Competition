'use strict';
app.factory('penaltyService', function ($http) {

    var serviceBase = 'http://localhost:52336/';
    var penaltyServiceFactory = {};

    // Grąžinti sąrašą
    var _getPenaltyList = function () {

        return $http.get(serviceBase + "api/penalty").then(function (results) {
            return results;
        });
    };

    // Pridėti naują objektą
    var _add = function (Penalty) {
        var request = $http({
            method: 'post',
            url: serviceBase + "api/penalty",
            data: Penalty
        });
        return request;
    }

    // Grąžinti vieną objektą
    var _get = function (Id) {
        return $http.get(serviceBase + "api/penalty/" + Id);
    }

    //Atnaujinti vieną objektą
   var _update = function (Id, Penalty) {
        var updaterequest = $http({
            method: 'put',
            url: serviceBase + "api/penalty" + Id,
            data: Penalty
        });

        return updaterequest;
    }

    // Ištrinti objektą
    var _delete = function (Id) {
        debugger;
        var deleterequest = $http({
            method: 'delete',
            url: seriveBase + "api/penalty/" + Id
        });
        return deleterequest;
    }

    penaltyServiceFactory.getPenaltyList = _getPenaltyList;
    penaltyServiceFactory.add = _add;
    penaltyServiceFactory.get = _get;
    penaltyServiceFactory.update = _update;
    penaltyServiceFactory.delete = _delete;

    return penaltyServiceFactory;

});