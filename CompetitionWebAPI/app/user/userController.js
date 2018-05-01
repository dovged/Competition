'use strict';
app.controller('userController', ['$scope', 'userService', function ($scope, userService) {

    $scope.team = {
        Id: '',
        Name: '',
        TeamCaptainId: ''
    };
    $scope.compClim = {};
    $scope.compKKT = {};
    $scope.clubs = {};
    $scope.user = {
        Id: '',
        Name: '',
        LastName: '',
        TelNumber: '',
        Email: '',
        ClubId: '',
        TeamId: '',
        UserId: '',
        Active: '',
        TrainerId: '',
        BirthYear: '',
        Lytis: ''
    };
    $scope.userNoteam = {};
    $scope.members = {};
    $scope.noTeam = false;
    $scope.noClimb = false;
    $scope.noKKT = false;
    $scope.newMember = {
        UserId: ''
    };

    loadUserInfo();

    // Užkraunami vartotojo duomenys
    function loadUserInfo() {
        /** Pagrindinė vartotojo informacija*/
        userService.getUser().then(function (results) {
            var u = results.data;
            $scope.user.Id = u.Id;
            $scope.user.Name = u.Name;
            $scope.user.LastName = u.LastName;
            $scope.user.TelNumber = u.TelNumber;
            $scope.user.Email = u.Email;
            $scope.user.ClubId = u.ClubId;
            $scope.user.TeamId = u.TeamId;
            $scope.user.UserId = u.UserId;
            $scope.user.Active = u.Active;
            $scope.user.TrainerId = u.TrainerId;
            $scope.user.BirthYear = u.BirthYear2;
            $scope.user.Lytis = u.Lytis;
        });

        /** Gaunami komandos duomenys*/
        userService.getUserTeam().then(function (results) {
            $scope.team = results.data;
            $scope.noTeam = true;
        });

        if (!$scope.noTeam) {
            userService.getUserNoTeam().then(function (results) {
                $scope.userNoteam = results.data;
            });
        }

        /** LAIPIOJIMO varžybų sąrašas, kuriose vartootjas dalyvauja*/
        userService.getCompListClim().then(function (results) {
            $scope.compClim = results.data;
            $scope.noClimb = true;
        });

        /** KKT varžybų sąrašas, kuriose vartotojas dalyvauja*/
        userService.getCompListKKT().then(function (results) {
            $scope.compKKT = results.data;
            $scope.noKKT = true;
        });

        /** Klubų sąrašas, atnaujinui skirta*/
        userService.getClubs().then(function (results) {
            $scope.clubs = results.data;
        });
    }

    // Panaikinama varžybų registracija LAIPIOJIMAS
    $scope.deleteCompClim = function (Id) {
        userService.deleteCompClim(Id).then(function (results) {
            loadUserInfo();
        });
    };

    // Panaikinama varžybų registracija KKT
    $scope.deleteCompKKT = function (Id) {
        userService.deleteCompKKT(Id).then(function (results) {
            loadUserInfo();
        });
    };

    //Atnaujinama vartotojo informacija
    $scope.update = function () {
        var u = {
            Id: $scope.user.Id,
            Name: $scope.user.Name,
            LastName: $scope.user.LastName,
            TelNumber: $scope.user.TelNumber,
            Email: $scope.user.Email,
            ClubId: $scope.user.ClubId,
            TeamId: $scope.user.TeamId,
            UserId: $scope.user.UserId,
            Active: $scope.user.Active,
            TrainerId: $scope.user.TrainerId,
            BirthYear: $scope.user.BirthYear,
            Lytis: $scope.user.Lytis
        };

        userService.updateUser(u);
    };

    //Pridėti narį į komandą
    $scope.addMember = function () {
        userService.addMember($scope.newMember.UserId, $scope.team.Id).then(function (results) {
            loadUserInfo();
        });
    };

    //Išmesti narį iš komandos
    $scope.removeMember = function (id) {
        userService.removeMember(id).then(function (results) {
            loadUserInfo();
        });
    };

    // Pridėti komandą
    $scope.addTeam = function () {
        var t = {
            Name: $scope.team.Name,
            TeamCaptainId: $scope.user.Id
        }
        userService.addTeam(t).then(function (results) {
            loadUserInfo();
        });
    };

}]);