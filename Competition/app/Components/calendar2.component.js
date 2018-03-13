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
var global_1 = require("../Shared/global");
var DataService_1 = require("../Shared/DataService");
var router_1 = require("@angular/router");
var Calendar2Component = (function () {
    function Calendar2Component(fb, _calendarService, _dataService, _router) {
        this.fb = fb;
        this._calendarService = _calendarService;
        this._dataService = _dataService;
        this._router = _router;
        this.indLoading = false;
    }
    Calendar2Component.prototype.ngOnInit = function () {
        this.LoadCompetitions();
    };
    Calendar2Component.prototype.LoadCompetitions = function () {
        var _this = this;
        this.indLoading = true;
        this._calendarService.get(global_1.Global.BASE_COMPETITION_ENDPOINT)
            .subscribe(function (comps) { _this.comps = comps; _this.indLoading = false; }, function (error) { return _this.msg = error; });
    };
    Calendar2Component.prototype.getRoutes = function (id) {
        this._dataService.saveData(id);
        this._router.navigate(['/routes']);
    };
    Calendar2Component.prototype.getTeams = function (id) {
        this._dataService.saveData(id);
        this._router.navigate(['/compteams']);
    };
    Calendar2Component.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.compFrm.enable() : this.compFrm.disable();
    };
    return Calendar2Component;
}());
__decorate([
    core_1.ViewChild('modal'),
    __metadata("design:type", ng2_bs3_modal_1.ModalComponent)
], Calendar2Component.prototype, "modal", void 0);
Calendar2Component = __decorate([
    core_1.Component({
        templateUrl: 'app/Components/calendar2.component.html',
        styleUrls: ['app/Components/calendar.component.css']
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder, calendor_service_1.CalendarService, DataService_1.DataService, router_1.Router])
], Calendar2Component);
exports.Calendar2Component = Calendar2Component;
//# sourceMappingURL=calendar2.component.js.map