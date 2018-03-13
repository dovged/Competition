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
var results_service_1 = require("../Service/results.service");
var forms_1 = require("@angular/forms");
var ng2_bs3_modal_1 = require("ng2-bs3-modal/ng2-bs3-modal");
var global_1 = require("../Shared/global");
var DataService_1 = require("../Shared/DataService");
var router_1 = require("@angular/router");
var ResultsComponent = (function () {
    function ResultsComponent(fb, _resultsService, _dataService, _router) {
        this.fb = fb;
        this._resultsService = _resultsService;
        this._dataService = _dataService;
        this._router = _router;
        this.indLoading = false;
    }
    ResultsComponent.prototype.ngOnInit = function () {
        this.LoadResults();
    };
    ResultsComponent.prototype.LoadResults = function () {
        var _this = this;
        this.indLoading = true;
        this.Id = this._dataService.getData();
        this._resultsService.get(global_1.Global.BASE_RESULTS_ENDPOINT, this.Id)
            .subscribe(function (results) { _this.results = results; _this.indLoading = false; }, function (error) { return _this.msg = error; });
    };
    ResultsComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.userFrm.enable() : this.userFrm.disable();
    };
    return ResultsComponent;
}());
__decorate([
    core_1.ViewChild('modal'),
    __metadata("design:type", ng2_bs3_modal_1.ModalComponent)
], ResultsComponent.prototype, "modal", void 0);
ResultsComponent = __decorate([
    core_1.Component({
        templateUrl: 'app/Components/results.component.html'
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder, results_service_1.ResultsService, DataService_1.DataService, router_1.Router])
], ResultsComponent);
exports.ResultsComponent = ResultsComponent;
//# sourceMappingURL=results.component.js.map