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
var route_service_1 = require("../Service/route.service");
var forms_1 = require("@angular/forms");
var ng2_bs3_modal_1 = require("ng2-bs3-modal/ng2-bs3-modal");
var enum_1 = require("../Shared/enum");
var global_1 = require("../Shared/global");
var DataService_1 = require("../Shared/DataService");
var router_1 = require("@angular/router");
var RouteComponent = (function () {
    function RouteComponent(fb, _routeService, _dataService, _router) {
        this.fb = fb;
        this._routeService = _routeService;
        this._dataService = _dataService;
        this._router = _router;
        this.indLoading = false;
    }
    RouteComponent.prototype.ngOnInit = function () {
        this.routeFrm = this.fb.group({
            Id: [''],
            Name: ['', forms_1.Validators.required],
            Points: ['', forms_1.Validators.required],
            Time: ['', forms_1.Validators.required]
        });
        this.LoadRoute();
    };
    RouteComponent.prototype.LoadRoute = function () {
        var _this = this;
        this.indLoading = true;
        this.Id = this._dataService.getData();
        this._routeService.get(global_1.Global.BASE_ROUTE_ENDPOINT)
            .subscribe(function (routes) { _this.routes = routes; _this.indLoading = false; }, function (error) { return _this.msg = error; });
        var t = 0;
        /* for (var item of this.routesAll) {
             if (item.CompId == this.Id) {
                 this.routes[0] = item;
                 t++;
             }
         }*/
    };
    RouteComponent.prototype.addRoute = function () {
        this.dbops = enum_1.DBOperation.create;
        this.SetControlsState(true);
        this.modalTitle = "Add Newr";
        this.modalBtnTitle = "Add";
        this.routeFrm.reset();
        this.modal.open();
    };
    RouteComponent.prototype.editRoute = function (id) {
        this.dbops = enum_1.DBOperation.update;
        this.SetControlsState(true);
        this.modalTitle = "Edit User";
        this.modalBtnTitle = "Update";
        this.route = this.routes.filter(function (x) { return x.Id == id; })[0];
        this.routeFrm.setValue(this.route);
        this.modal.open();
    };
    RouteComponent.prototype.deleteRoute = function (id) {
        this.dbops = enum_1.DBOperation.delete;
        this.SetControlsState(false);
        this.modalTitle = "Confirm to Delete?";
        this.modalBtnTitle = "Delete";
        this.route = this.routes.filter(function (x) { return x.Id == id; })[0];
        this.routeFrm.setValue(this.route);
        this.modal.open();
    };
    RouteComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        this.msg = "";
        switch (this.dbops) {
            case enum_1.DBOperation.create:
                this._routeService.post(global_1.Global.BASE_ROUTE_ENDPOINT, formData._value).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Data successfully added.";
                        _this.LoadRoute();
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
                this._routeService.put(global_1.Global.BASE_ROUTE_ENDPOINT, formData._value.Id, formData._value).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Data successfully updated.";
                        _this.LoadRoute();
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
                this._routeService.delete(global_1.Global.BASE_ROUTE_ENDPOINT, formData._value.Id).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Data successfully deleted.";
                        _this.LoadRoute();
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
    RouteComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.routeFrm.enable() : this.routeFrm.disable();
    };
    return RouteComponent;
}());
__decorate([
    core_1.ViewChild('modal'),
    __metadata("design:type", ng2_bs3_modal_1.ModalComponent)
], RouteComponent.prototype, "modal", void 0);
RouteComponent = __decorate([
    core_1.Component({
        templateUrl: 'app/Components/route.component.html',
        styleUrls: ['app/Components/user.component.css']
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder, route_service_1.RouteService, DataService_1.DataService, router_1.Router])
], RouteComponent);
exports.RouteComponent = RouteComponent;
//# sourceMappingURL=route.component.js.map