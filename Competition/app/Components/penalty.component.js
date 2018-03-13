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
var penalty_service_1 = require("../Service/penalty.service");
var forms_1 = require("@angular/forms");
var ng2_bs3_modal_1 = require("ng2-bs3-modal/ng2-bs3-modal");
var enum_1 = require("../Shared/enum");
var global_1 = require("../Shared/global");
var PenaltyComponent = (function () {
    function PenaltyComponent(fb, _penaltyService) {
        this.fb = fb;
        this._penaltyService = _penaltyService;
        this.indLoading = false;
    }
    PenaltyComponent.prototype.ngOnInit = function () {
        this.penaltyFrm = this.fb.group({
            Id: [''],
            Name: ['', forms_1.Validators.required],
            Points: ['', forms_1.Validators.required]
        });
        this.LoadPenalty();
    };
    PenaltyComponent.prototype.LoadPenalty = function () {
        var _this = this;
        this.indLoading = true;
        this._penaltyService.get(global_1.Global.BASE_PENALTY_ENDPOINT)
            .subscribe(function (penalties) { _this.penalties = penalties; _this.indLoading = false; }, function (error) { return _this.msg = error; });
    };
    PenaltyComponent.prototype.addPenalty = function () {
        this.dbops = enum_1.DBOperation.create;
        this.SetControlsState(true);
        this.modalTitle = "Add New User";
        this.modalBtnTitle = "Add";
        this.penaltyFrm.reset();
        this.modal.open();
    };
    PenaltyComponent.prototype.editPenalty = function (id) {
        this.dbops = enum_1.DBOperation.update;
        this.SetControlsState(true);
        this.modalTitle = "Edit User";
        this.modalBtnTitle = "Update";
        this.penalty = this.penalties.filter(function (x) { return x.Id == id; })[0];
        this.penaltyFrm.setValue(this.penalty);
        this.modal.open();
    };
    PenaltyComponent.prototype.deletePenalty = function (id) {
        this.dbops = enum_1.DBOperation.delete;
        this.SetControlsState(false);
        this.modalTitle = "Confirm to Delete?";
        this.modalBtnTitle = "Delete";
        this.penalty = this.penalties.filter(function (x) { return x.Id == id; })[0];
        this.penaltyFrm.setValue(this.penalty);
        this.modal.open();
    };
    PenaltyComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        this.msg = "";
        switch (this.dbops) {
            case enum_1.DBOperation.create:
                this._penaltyService.post(global_1.Global.BASE_PENALTY_ENDPOINT, formData._value).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Data successfully added.";
                        _this.LoadPenalty();
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
                this._penaltyService.put(global_1.Global.BASE_PENALTY_ENDPOINT, formData._value.Id, formData._value).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Data successfully updated.";
                        _this.LoadPenalty();
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
                this._penaltyService.delete(global_1.Global.BASE_PENALTY_ENDPOINT, formData._value.Id).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Data successfully deleted.";
                        _this.LoadPenalty();
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
    PenaltyComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.penaltyFrm.enable() : this.penaltyFrm.disable();
    };
    return PenaltyComponent;
}());
__decorate([
    core_1.ViewChild('modal'),
    __metadata("design:type", ng2_bs3_modal_1.ModalComponent)
], PenaltyComponent.prototype, "modal", void 0);
PenaltyComponent = __decorate([
    core_1.Component({
        templateUrl: 'app/Components/penalty.component.html',
        styleUrls: ['app/Components/user.component.css']
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder, penalty_service_1.PenaltyService])
], PenaltyComponent);
exports.PenaltyComponent = PenaltyComponent;
//# sourceMappingURL=penalty.component.js.map