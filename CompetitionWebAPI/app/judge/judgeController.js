'use strict';
app.controller('judgeController', ['$scope', 'judgeService', '$location', 'localStorageService',
    function ($scope, judgeService, $location, localStorageService) {

        $scope.competitionList = [];
        $scope.NoComp = false;

        // užkraunamas varžybų sąrašas, kuriose gali vartotoajs teisėjauti
        judgeService.getCompetitionList().then(function (results) {
            $scope.competitionList = results.data;
            $scope.NoComp = true;
            localStorageService.remove("CompjudgeId");
        });

        /** Perėjimas į teisėjo lapo pildymo langą pasirinktoms varžyboms*/
        $scope.judgePaper = function (Id, type, ClimbType) {
            localStorageService.set("CompjudgeId", Id);
            // LAIPIOJIMO VARŽYBŲ ATVEJU
            if (type) {
                if (ClimbType == 3) {
                    $location.path("/judgesPapersClim1");
                }
                else {
                    $location.path("/judgesPapersClim2");
                }
                
            }
            // KKT VARŽYBŲ ATVEJU
            else {
                $location.path("/judgesPapersKKT");
            }
        };

    }]);