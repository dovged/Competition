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
var account_service_1 = require("../Service/account.service");
var forms_1 = require("@angular/forms");
var ng2_bs3_modal_1 = require("ng2-bs3-modal/ng2-bs3-modal");
var global_1 = require("../Shared/global");
var AccountComponent = (function () {
    function AccountComponent(fb, _accountService) {
        this.fb = fb;
        this._accountService = _accountService;
        this.indLoading = false;
    }
    AccountComponent.prototype.ngOnInit = function () {
        this.accountFrm = this.fb.group({
            Id: [''],
            Name: ['', forms_1.Validators.required],
            Points: ['', forms_1.Validators.required]
        });
        this.LoadAccount();
    };
    AccountComponent.prototype.LoadAccount = function () {
        var _this = this;
        this.indLoading = true;
        this.Id = 3;
        this._accountService.getid(global_1.Global.BASE_USER_ENDPOINT, this.Id)
            .subscribe(function (user) { _this.user = user; _this.indLoading = false; }, function (error) { return _this.msg = error; });
    };
    return AccountComponent;
}());
__decorate([
    core_1.ViewChild('modal'),
    __metadata("design:type", ng2_bs3_modal_1.ModalComponent)
], AccountComponent.prototype, "modal", void 0);
AccountComponent = __decorate([
    core_1.Component({
        templateUrl: 'app/Components/account.component.html',
        styleUrls: ['app/Components/user.component.css']
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder, account_service_1.AccountService])
], AccountComponent);
exports.AccountComponent = AccountComponent;
//# sourceMappingURL=accout.component.js.map