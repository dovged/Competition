'use strict';
app.factory('authService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {

    var serviceBase = 'http://localhost:52336/';
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: "",
        isAdmin: false,
        isKKTDalyvis: false,
        isTreneris: false,
        isKKTTreneis: false,
        isOrg: false,
        isKKTOrg: false
    };

    var _saveRegistration = function (registration) {

        _logOut();

        return $http.post(serviceBase + 'api/account/register', registration).then(function (response) {
            return response;
        });

    };

    var _login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

            localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName });

            _authentication.isAuth = true;
            _authentication.userName = loginData.userName;
          //  _isAdmin(loginData.userName);

            deferred.resolve(response);

        }).error(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _logOut = function () {

        localStorageService.remove('authorizationData');

        _authentication.isAuth = false;
        _authentication.userName = "";
        _authentication.isAdmin = false;
        _authentication.isKKTDalyvis = false;
        _authentication.isTreneris = false;
        _authentication.isKKTTreneris = false;
        _authentication.isOrg = false;
        _authentication.isKKTOrg = false;

    };

    var _fillAuthData = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
        }

       // _isAdmin(authData.userName);

    }

    var _isAdmin = function (userName, role) {
        $http.post(serviceBase + 'api/role/' + user + '/7').success(function (response) {
            _authentication.isAdmin = true;

        }).error(function (err, status) {
            _authentication.isAdmin = false;
        });
    }

    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;

    return authServiceFactory;
}]);