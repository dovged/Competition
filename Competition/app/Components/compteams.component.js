"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var compTeam_service_1 = require("../Service/compTeam.service");
var forms_1 = require("@angular/forms");
var ng2_bs3_modal_1 = require("ng2-bs3-modal/ng2-bs3-modal");
var global_1 = require("../Shared/global");
var DataService_1 = require("../Shared/DataService");
var router_1 = require("@angular/router");
var CompTeamsComponent = (function () {
    function CompTeamsComponent(fb, _userService, _dataService, _router) {
        this.fb = fb;
        this._userService = _userService;
        this._dataService = _dataService;
        this._router = _router;
        this.indLoading = false;
    }
    CompTeamsComponent.prototype.ngOnInit = function () {
        this.LoadCompTeams();
    };
    CompTeamsComponent.prototype.LoadCompTeams = function () {
        var _this = this;
        this.indLoading = true;
        this.Id = this._dataService.getData();
        this._userService.get(global_1.Global.BASE_COMPTEAM_ENDPOINT, this.Id)
            .subscribe(function (compteams) { _this.compteams = compteams; _this.indLoading = false; }, function (error) { return _this.msg = error; });
    };
    CompTeamsComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.userFrm.enable() : this.userFrm.disable();
    };
    return CompTeamsComponent;
}());
__decorate([
    core_1.ViewChild('modal'),
    __metadata("design:type", ng2_bs3_modal_1.ModalComponent)
], CompTeamsComponent.prototype, "modal", void 0);
CompTeamsComponent = __decorate([
    core_1.Component({
        templateUrl: 'app/Components/compteams.component.html',
        styleUrls: ['app/Components/user.component.css']
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder, compTeam_service_1.CompTeamService, DataService_1.DataService, router_1.Router])
], CompTeamsComponent);
exports.CompTeamsComponent = CompTeamsComponent;
//# sourceMappingURL=compteams.component.js.map