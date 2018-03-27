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
var calendor_service_1 = require("../Service/calendor.service");
var forms_1 = require("@angular/forms");
var ng2_bs3_modal_1 = require("ng2-bs3-modal/ng2-bs3-modal");
var enum_1 = require("../Shared/enum");
var global_1 = require("../Shared/global");
var DataService_1 = require("../Shared/DataService");
var router_1 = require("@angular/router");
var CalendarForOrgComponent = (function () {
    function CalendarForOrgComponent(fb, _calendarService, _dataService, _router) {
        this.fb = fb;
        this._calendarService = _calendarService;
        this._dataService = _dataService;
        this._router = _router;
        this.indLoading = false;
    }
    CalendarForOrgComponent.prototype.ngOnInit = function () {
        this.LoadCompetitions();
    };
    CalendarForOrgComponent.prototype.LoadCompetitions = function () {
        var _this = this;
        this.indLoading = true;
        this._calendarService.get(global_1.Global.BASE_COMPETITION_ENDPOINT)
            .subscribe(function (comps) { _this.comps = comps; _this.indLoading = false; }, function (error) { return _this.msg = error; });
    };
    CalendarForOrgComponent.prototype.addCompetition = function () {
        this.dbops = enum_1.DBOperation.create;
        this.SetControlsState(true);
        this.modalTitle = "Regsitruoti varžybas";
        this.modalBtnTitle = "Registruoti";
        this.compFrm.reset();
        this.modal.open();
    };
    CalendarForOrgComponent.prototype.getRoutes = function (id) {
        this._dataService.saveData(id);
        this._router.navigate(['/routes']);
    };
    CalendarForOrgComponent.prototype.getTeams = function (id) {
        this._dataService.saveData(id);
        this._router.navigate(['/compteams']);
    };
    CalendarForOrgComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.compFrm.enable() : this.compFrm.disable();
    };
    return CalendarForOrgComponent;
}());
__decorate([
    core_1.ViewChild('modal'),
    __metadata("design:type", ng2_bs3_modal_1.ModalComponent)
], CalendarForOrgComponent.prototype, "modal", void 0);
CalendarForOrgComponent = __decorate([
    core_1.Component({
        templateUrl: 'app/Components/calendarForOrg.component.html',
        styleUrls: ['app/Components/calendar.component.css']
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder, calendor_service_1.CalendarService, DataService_1.DataService, router_1.Router])
], CalendarForOrgComponent);
exports.CalendarForOrgComponent = CalendarForOrgComponent;
//# sourceMappingURL=calendarForOrg.component.js.map