'use strict';
app.factory('authService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {

    var serviceBase = 'http://localhost:52336/';
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: "",
        isAdmin: false,
        isTreneris: false,
        isOrg: false
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
            _isAdmin(loginData.userName);
            _isTreneris(loginData.userName);
            _isOrg(loginData.userName);

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
        _authentication.isTreneris = false;
        _authentication.isOrg = false;

    };

    var _fillAuthData = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
            _isAdmin(authData.userName);
            _isTreneris(authData.userName);
            _isOrg(authData.userName);
        }
    }
    // Patriktina ar vartotojas turi Admin rolę
    var _isAdmin = function (userName, role) {
        $http.get(serviceBase + 'api/role/' + userName + '/7').success(function (response) {
            _authentication.isAdmin = true;

        }).error(function (err, status) {
            _authentication.isAdmin = false;
        });
    }

    // Patikrina ar vartotojas turi Trenerio arba KKT Trenio rolę
    var _isTreneris = function (userName, role) {
        $http.get(serviceBase + 'api/role/' + userName + '/20').success(function (response) {
            _authentication.isTreneris = true;

        }).error(function (err, status) {
            _authentication.isTreneris = false;
        });
    }

    // Patikrina ar vartotojas turi Org arba KKT Org rolę
    var _isOrg = function (userName, role) {
        $http.get(serviceBase + 'api/role/' + userName + '/30').success(function (response) {
            _authentication.isOrg = true;

        }).error(function (err, status) {
            _authentication.isOrg = false;
        });
    }

    // Užregistruojama vartotojo asmeninė informacija
    var _addUserInfo = function (userName, user) {
        var addrequest = $http({
            method: 'post',
            url: serviceBase + "api/user/" + userName,
            data: user
        });

        return addrequest;
    };

    /** PRISKIRIMAI*/
    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;
    authServiceFactory.isAdmin = _isAdmin;
    authServiceFactory.isTreneris = _isTreneris;
    authServiceFactory.isOrg = _isOrg;
    authServiceFactory.addUserInfo = _addUserInfo;


    return authServiceFactory;
}]);