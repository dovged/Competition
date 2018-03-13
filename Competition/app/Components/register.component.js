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
var router_1 = require("@angular/router");
var user_service_1 = require("../Service/user.service");
var global_1 = require("../Shared/global");
var forms_1 = require("@angular/forms");
var RegisterComponent = (function () {
    function RegisterComponent(_router, _userService, fb) {
        this._router = _router;
        this._userService = _userService;
        this.fb = fb;
        this.model = {};
        this.loading = false;
    }
    RegisterComponent.prototype.ngOnInit = function () {
        this.userFrm = this.fb.group({
            UserName: ['', forms_1.Validators.required],
            Password: ['', forms_1.Validators.required],
            ComfirmPassword: ['', forms_1.Validators.required]
        });
    };
    RegisterComponent.prototype.register = function () {
        var _this = this;
        this.loading = true;
        this._userService.Register(global_1.Global.BASE_REGISTER_ENDPOINT, this.model)
            .subscribe(function (data) {
            _this.msg = "Registration successful";
            _this._router.navigate(['/login']);
        }, function (error) {
            _this.msg = error;
            _this.loading = false;
        });
    };
    return RegisterComponent;
}());
RegisterComponent = __decorate([
    core_1.Component({
        moduleId: module.id,
        templateUrl: 'register.component.html'
    }),
    __metadata("design:paramtypes", [router_1.Router,
        user_service_1.UserService,
        forms_1.FormBuilder])
], RegisterComponent);
exports.RegisterComponent = RegisterComponent;
//# sourceMappingURL=register.component.js.map