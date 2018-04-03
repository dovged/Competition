'use strict';
app.controller('competitionController', ['$scope','$location', 'competitionService', function ($scope, $location, competitionService) {

    $scope.competitionList = [];
    loadCompetitionList();

    // užkraunamas varžybų sąrašas;
    function loadCompetitionList() {
        competitionService.getCompetitionList().then(function (results) {
            $scope.competitionList = results.data;
        }, function (error) {
           // alert(error.data.message);
        });
    }

    $scope.CompInfo;

    $scope.getCompInfo = function (competition) {
        CompInfo = competition;
        $location.path("/compInfo")
    }
 
       
  
   

}]);