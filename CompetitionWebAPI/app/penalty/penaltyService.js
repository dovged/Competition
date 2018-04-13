'use strict';
app.factory('penaltyService', function ($http) {

    var serviceBase = 'http://localhost:52336/';
    var penaltyServiceFactory = {};

    // Grąžinti baudų sąrašą
    var _getPenaltyList = function () {

        return $http.get(serviceBase + "api/penalty").then(function (results) {
            return results;
        });
    };

    // Pridėti naują baudą
    var _add = function (Penalty) {
        var request = $http({
            method: 'post',
            url: serviceBase + "api/penalty",
            data: Penalty
        });
        return request;
    }

    // Grąžinti vienos baudos informaciją
    var _get = function (Id) {
        return $http.get(serviceBase + "api/penalty/" + Id);
    }

    // Atnaujinti vienos baudos informaciją
   var _update = function (Id, Penalty) {
        var updaterequest = $http({
            method: 'put',
            url: serviceBase + "api/penalty" + Id,
            data: Penalty
        });

        return updaterequest;
    }

    // Padaryti baudą neaktyvią
    var _delete = function (Id) {
        var deleterequest = $http({
            method: 'delete',
            url: serviceBase + "api/penalty/" + Id
        });
        return deleterequest;
    }

    /** PRISKIRIAMAI */
    penaltyServiceFactory.getPenaltyList = _getPenaltyList;
    penaltyServiceFactory.add = _add;
    penaltyServiceFactory.get = _get;
    penaltyServiceFactory.update = _update;
    penaltyServiceFactory.delete = _delete;

    return penaltyServiceFactory;

});