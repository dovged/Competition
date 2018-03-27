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
var CalendarForCompComponent = (function () {
    function CalendarForCompComponent(fb, _calendarService, _dataService, _router) {
        this.fb = fb;
        this._calendarService = _calendarService;
        this._dataService = _dataService;
        this._router = _router;
        this.indLoading = false;
    }
    CalendarForCompComponent.prototype.ngOnInit = function () {
        this.compFrm = this.fb.group({
            Id: [''],
            Name: ['', forms_1.Validators.required],
            Date: ['', forms_1.Validators.required],
            UserId: ['']
        });
        this.LoadCompetitions();
    };
    CalendarForCompComponent.prototype.LoadCompetitions = function () {
        var _this = this;
        this.indLoading = true;
        this._calendarService.get(global_1.Global.BASE_COMPETITION_ENDPOINT)
            .subscribe(function (comps) { _this.comps = comps; _this.indLoading = false; }, function (error) { return _this.msg = error; });
    };
    CalendarForCompComponent.prototype.registration = function (id, open) {
        if (open) {
            this.addRegistration(id);
        }
        else {
            // TO DO
        }
    };
    CalendarForCompComponent.prototype.addRegistration = function (id) {
        this.dbops = enum_1.DBOperation.create;
        this.SetControlsState(true);
        this.modalTitle = "Regsitruotis į varžybas";
        this.modalBtnTitle = "Registruotis";
        this.compFrm.reset();
        this.modal.open();
    };
    CalendarForCompComponent.prototype.getResults = function (id) {
        this._dataService.saveData(id);
        this._router.navigate(['/results']);
    };
    CalendarForCompComponent.prototype.onSubmit = function (formData) {
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
    CalendarForCompComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.compFrm.enable() : this.compFrm.disable();
    };
    return CalendarForCompComponent;
}());
__decorate([
    core_1.ViewChild('modal'),
    __metadata("design:type", ng2_bs3_modal_1.ModalComponent)
], CalendarForCompComponent.prototype, "modal", void 0);
CalendarForCompComponent = __decorate([
    core_1.Component({
        templateUrl: 'app/Components/calendarForComp.component.html',
        styleUrls: ['app/Components/calendar.component.css']
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder, calendor_service_1.CalendarService, DataService_1.DataService, router_1.Router])
], CalendarForCompComponent);
exports.CalendarForCompComponent = CalendarForCompComponent;
//# sourceMappingURL=calendarForComp.component.js.map