'use strict';
app.controller('competitionController', ['$scope', 'competitionService', function ($scope, competitionService) {

    $scope.competitionList = [];
  
    // užkraunamas varžybų sąrašas;
 
        competitionService.getCompetitionList().then(function (results) {
            $scope.competitionList = results.data;
        }, function (error) {
           // alert(error.data.message);
        });
  
   

}]);