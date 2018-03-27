"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var platform_browser_1 = require("@angular/platform-browser");
var forms_1 = require("@angular/forms");
var app_component_1 = require("./app.component");
var ng2_bs3_modal_1 = require("ng2-bs3-modal/ng2-bs3-modal");
var http_1 = require("@angular/http");
var app_routing_1 = require("./app.routing");
/** Components*/
var user_component_1 = require("./components/user.component");
var home_component_1 = require("./components/home.component");
var footer_component_1 = require("./footer/footer.component");
var calendarForComp_component_1 = require("./components/calendarForComp.component");
var calendarForOrg_component_1 = require("./components/calendarForOrg.component");
var penalty_component_1 = require("./components/penalty.component");
var results_component_1 = require("./components/results.component");
var route_component_1 = require("./components/route.component");
var compteams_component_1 = require("./components/compteams.component");
var login_component_1 = require("./components/login.component");
var register_component_1 = require("./components/register.component");
/* Services*/
var user_service_1 = require("./Service/user.service");
var calendor_service_1 = require("./Service/calendor.service");
var penalty_service_1 = require("./Service/penalty.service");
var DataService_1 = require("./Shared/DataService");
var results_service_1 = require("./Service/results.service");
var route_service_1 = require("./Service/route.service");
var compTeam_service_1 = require("./Service/compTeam.service");
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        imports: [platform_browser_1.BrowserModule, forms_1.ReactiveFormsModule, http_1.HttpModule, app_routing_1.routing, ng2_bs3_modal_1.Ng2Bs3ModalModule],
        declarations: [app_component_1.AppComponent, user_component_1.UserComponent, calendarForComp_component_1.CalendarForCompComponent, penalty_component_1.PenaltyComponent,
            home_component_1.HomeComponent, footer_component_1.FooterComponent, results_component_1.ResultsComponent, route_component_1.RouteComponent,
            calendarForOrg_component_1.CalendarForOrgComponent, compteams_component_1.CompTeamsComponent, login_component_1.LoginComponent, register_component_1.RegisterComponent],
        providers: [{ provide: common_1.APP_BASE_HREF, useValue: '/' }, user_service_1.UserService, penalty_service_1.PenaltyService, calendor_service_1.CalendarService, DataService_1.DataService, results_service_1.ResultsService, route_service_1.RouteService, compTeam_service_1.CompTeamService],
        bootstrap: [app_component_1.AppComponent]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map