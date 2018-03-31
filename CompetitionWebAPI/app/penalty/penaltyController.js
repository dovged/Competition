'use strict';
app.controller('penaltyController', ['$scope', 'penaltyService', function ($scope, penaltyService) {

    $scope.penaltyList = [];
    loadPenaltyList();

    // Užkraunami duomenys į lentelę
    function loadPenaltyList() {
         penaltyService.getPenaltyList().then(function (results) {

                $scope.penaltyList = results.data;

          }, function (error) {
                //alert(error.data.message);
         });
    }

    // Išsaugoti naują objektą
    $scope.add = function () {
        var Penalty = {
            Id: $scope.Id,
            Name: $scope.Name,
            Points: $scope.Points
        };

        penaltyService.add(Penalty).then(function (results) {
            $scope.Id = results.data.Id;
            loadPenalties();
        },
            function () {
                //
            });
    }

    //Grąžinti vieną objektą
    $scope.get = function (Id) {
        penaltyService.get(Id).then(function (results) {
            var p = results.data;
            $scope.UpdateId = p.Id;
            $scope.UpdateName = p.Name;
            $scope.UpdatePoints = p.Points;
        },
            function () {
                //
            });
    }

    // Atnaujinti objektą
    $scope.update = function () {
        var Penalty = {
            Id: $scope.UpdateId,
            Name: $scope.UpdateName,
            Points: $scope.UpdatePoints
        };
        debugger;
        penaltyService.update($scope.UpdateId, Penalty).then(function (results) {
            loadPenalties();
        },
            function () {
                //

            });
    }

    // Ištrinti objektą
    $scope.delete = function (UpdateId) {
        debugger;
        penaltyService.delete($scope.UpdateId).then(function (results) {
            var Penalty = {
                Id: '',
                Name: '',
                Points: ''
            };
            loadPenalties();
        });
    }


}]);