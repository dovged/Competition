
var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/orders", {
        controller: "ordersController",
        templateUrl: "/app/views/orders.html"
    });

    $routeProvider.when("/calendar", {
        controller: "calendarController",
        templateUrl: "/app/calendar/calendar.html"
    });

    $routeProvider.when("/user", {
        controller: "userController",
        templateUrl: "app/views/user.html"
    });

    $routeProvider.when("/penalties", {
        controller: "penaltyController",
        templateUrl: "app/penalty/penalties.html"
    });

    $routeProvider.when("/addNew", {
        controller: "penaltyController",
        templateUrl: "app/views/penaltyModal.html"
    });

    $routeProvider.when("/competitionOrg", {
        controller: "competitionController",
        templateUrl: "app/competition/competitionList.html"
    });

    $routeProvider.when("/admin", {
        controller: "adminController",
        templateUrl: "app/admin/userList.html"
    });

    $routeProvider.when("/trainer", {
        controller: "trainerController",
        templateUrl: "app/trainer/trainerMain.html"
    });

    $routeProvider.when("/results", {
        controller: "resultsController",
        templateUrl: "app/calendar/results.html"
    });

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
