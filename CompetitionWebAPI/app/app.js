
var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });
     /** USER PASIEKIAMI LANGAI*/
    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    /** DALYVIO PASIEKIAMI LANGAI*/
    $routeProvider.when("/calendar", {
        controller: "calendarController",
        templateUrl: "/app/calendar/calendar.html"
    });

    $routeProvider.when("/user", {
        controller: "userController",
        templateUrl: "app/user/user.html"
    });

    $routeProvider.when("/results", {
        controller: "resultsController",
        templateUrl: "app/calendar/results.html"
    });

    $routeProvider.when("/competitorClim", {
        controller: "competitorClimController",
        templateUrl: "app/calendar/competitorClim.html"
    });

    $routeProvider.when("/competitorKKT", {
        controller: "competitorKKTController",
        templateUrl: "app/calendar/competitorKKT.html"
    });

    $routeProvider.when("/judges", {
        controller: "judgeController",
        templateUrl: "app/judge/judge.html"
    });

    $routeProvider.when("/judgesPapersClim1", {
        controller: "judgesPapersClim1Controller",
        templateUrl: "app/judge/judgesPapersClim1.html"
    });

    $routeProvider.when("/judgesPapersClim2", {
        controller: "judgesPapersClim2Controller",
        templateUrl: "app/judge/judgesPapersClim2.html"
    });

    $routeProvider.when("/judgesPapersKKT", {
        controller: "judgesPapersKKTController",
        templateUrl: "app/judge/judgesPapersKKT.html"
    });

    /** VARŽYBŲ ORGANIZATORIAUS PASIEKIAMI LAGAI*/
    $routeProvider.when("/penalties", {
        controller: "penaltyController",
        templateUrl: "app/penalty/penalties.html"
    });

    $routeProvider.when("/addPenalty", {
        controller: "addPenaltyController",
        templateUrl: "app/penalty/addPenalty.html"
    });

    $routeProvider.when("/updatePenalty", {
        controller: "updatePenaltyController",
        templateUrl: "app/penalty/updatePenalty.html"
    });

    $routeProvider.when("/competitionOrg", {
        controller: "competitionController",
        templateUrl: "app/competition/competitionList.html"
    });

    $routeProvider.when("/compInfo", {
        controller: "competitionInfoController",
        templateUrl: "app/competition/competitionInfo.html"
    });

    $routeProvider.when("/nonPaidKKT", {
        controller: "nonPaidKKTController",
        templateUrl: "app/competition/nonPaidKKT.html"
    });

    $routeProvider.when("/nonPaidClim", {
        controller: "nonPaidClimController",
        templateUrl: "app/competition/nonPaidClim.html"
    });

    $routeProvider.when("/addKKTRoute", {
        controller: "addKKTRouteController",
        templateUrl: "app/competition/addKKTRoute.html"
    });

    $routeProvider.when("/updateKKTRoute", {
        controller: "updateKKTRouteController",
        templateUrl: "app/competition/updateKKTRoute.html"
    });

    $routeProvider.when("/addCompetition", {
        controller: "addCompetitionController",
        templateUrl: "app/competition/addCompetition.html"
    });

    /** ADMINISTRATORIAUS PASIEKIAMI LANGAI*/
    $routeProvider.when("/admin", {
        controller: "adminController",
        templateUrl: "app/admin/userList.html"
    });

    /** TRENERIO PASIEKIAMI LANGAI*/
    $routeProvider.when("/trainer", {
        controller: "trainerController",
        templateUrl: "app/trainer/trainerMain.html"
    });

    $routeProvider.when("/addTrainee", {
        controller: "addTraineeController",
        templateUrl: "app/trainer/addTrainee.html"
    });

    $routeProvider.when("/compTrainer", {
        controller: "compTrainerController",
        templateUrl: "app/trainer/compTrainer.html"
    });

    $routeProvider.when("/registerCompKKT", {
        controller: "trainerRegisterKKTController",
        templateUrl: "app/trainer/trainerRegisterKKT.html"
    });

    $routeProvider.when("/registerCompClim", {
        controller: "trainerRegisterClimController",
        templateUrl: "app/trainer/trainerRegisterClim.html"
    });

    $routeProvider.when("/updateTrainee", {
        controller: "updateTraineeController",
        templateUrl: "app/trainer/updateTrainee.html"
    })


    $routeProvider.otherwise({ redirectTo: "/home" });

});

var serviceBase = 'http://localhost:52336/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);
