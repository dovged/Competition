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
var CalendarComponent = (function () {
    function CalendarComponent(fb, _calendarService, _dataService, _router) {
        this.fb = fb;
        this._calendarService = _calendarService;
        this._dataService = _dataService;
        this._router = _router;
        this.indLoading = false;
    }
    CalendarComponent.prototype.ngOnInit = function () {
        this.compFrm = this.fb.group({
            Id: [''],
            Name: ['', forms_1.Validators.required],
            Date: ['', forms_1.Validators.required],
            UserId: ['']
        });
        this.LoadCompetitions();
    };
    CalendarComponent.prototype.LoadCompetitions = function () {
        var _this = this;
        this.indLoading = true;
        this._calendarService.get(global_1.Global.BASE_COMPETITION_ENDPOINT)
            .subscribe(function (comps) { _this.comps = comps; _this.indLoading = false; }, function (error) { return _this.msg = error; });
    };
    CalendarComponent.prototype.addCompetition = function () {
        this.dbops = enum_1.DBOperation.create;
        this.SetControlsState(true);
        this.modalTitle = "Regsitruoti varžybas";
        this.modalBtnTitle = "Registruoti";
        this.compFrm.reset();
        this.modal.open();
    };
    CalendarComponent.prototype.editComp = function (id) {
        this.dbops = enum_1.DBOperation.update;
        this.SetControlsState(true);
        this.modalTitle = "Redaguoti varžybų informaciją";
        this.modalBtnTitle = "Redaguoti";
        this.comp = this.comps.filter(function (x) { return x.Id == id; })[0];
        this.compFrm.setValue(this.comp);
        this.modal.open();
    };
    CalendarComponent.prototype.deleteComp = function (id) {
        this.dbops = enum_1.DBOperation.delete;
        this.SetControlsState(false);
        this.modalTitle = "Patvirtinke naikinimą?";
        this.modalBtnTitle = "Naikinti";
        this.comp = this.comps.filter(function (x) { return x.Id == id; })[0];
        this.compFrm.setValue(this.comp);
        this.modal.open();
    };
    CalendarComponent.prototype.getResults = function (id) {
        this._dataService.saveData(id);
        this._router.navigate(['/results']);
    };
    CalendarComponent.prototype.getRoutes = function (id) {
        this._dataService.saveData(id);
        this._router.navigate(['/routes']);
    };
    CalendarComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        this.msg = "";
        switch (this.dbops) {
            case enum_1.DBOperation.create:
                this._calendarService.post(global_1.Global.BASE_COMPETITION_ENDPOINT, formData._value).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Duomenys sėkmingai išsaugoti.";
                        _this.LoadCompetitions();
                    }
                    else {
                        _this.msg = "There is some issue in saving records, please contact to system administrator!";
                    }
                    _this.modal.dismiss();
                }, function (error) {
                    _this.msg = error;
                });
                break;
            case enum_1.DBOperation.update:
                this._calendarService.put(global_1.Global.BASE_COMPETITION_ENDPOINT, formData._value.Id, formData._value).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Data successfully updated.";
                        _this.LoadCompetitions();
                    }
                    else {
                        _this.msg = "There is some issue in saving records, please contact to system administrator!";
                    }
                    _this.modal.dismiss();
                }, function (error) {
                    _this.msg = error;
                });
                break;
            case enum_1.DBOperation.delete:
                this._calendarService.delete(global_1.Global.BASE_COMPETITION_ENDPOINT, formData._value.Id).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Data successfully deleted.";
                        _this.LoadCompetitions();
                    }
                    else {
                        _this.msg = "There is some issue in saving records, please contact to system administrator!";
                    }
                    _this.modal.dismiss();
                }, function (error) {
                    _this.msg = error;
                });
                break;
        }
    };
    CalendarComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.compFrm.enable() : this.compFrm.disable();
    };
    return CalendarComponent;
}());
__decorate([
    core_1.ViewChild('modal'),
    __metadata("design:type", ng2_bs3_modal_1.ModalComponent)
], CalendarComponent.prototype, "modal", void 0);
CalendarComponent = __decorate([
    core_1.Component({
        templateUrl: 'app/Components/calendar1.component.html',
        styleUrls: ['app/Components/calendar.component.css']
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder, calendor_service_1.CalendarService, DataService_1.DataService, router_1.Router])
], CalendarComponent);
exports.CalendarComponent = CalendarComponent;
//# sourceMappingURL=calendar.component.js.map