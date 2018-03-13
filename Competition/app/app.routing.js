"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var user_component_1 = require("./components/user.component");
var home_component_1 = require("./components/home.component");
var calendar_component_1 = require("./components/calendar.component");
var calendar2_component_1 = require("./components/calendar2.component");
var penalty_component_1 = require("./components/penalty.component");
var results_component_1 = require("./components/results.component");
var route_component_1 = require("./components/route.component");
var compteams_component_1 = require("./components/compteams.component");
var login_component_1 = require("./components/login.component");
var register_component_1 = require("./components/register.component");
var appRoutes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: home_component_1.HomeComponent },
    { path: 'admin', component: user_component_1.UserComponent },
    { path: 'calendar', component: calendar_component_1.CalendarComponent },
    { path: 'penalty', component: penalty_component_1.PenaltyComponent },
    { path: 'results', component: results_component_1.ResultsComponent },
    { path: 'routes', component: route_component_1.RouteComponent },
    { path: 'calendarJP', component: calendar2_component_1.Calendar2Component },
    { path: 'compteams', component: compteams_component_1.CompTeamsComponent },
    { path: 'register', component: register_component_1.RegisterComponent },
    { path: 'login', component: login_component_1.LoginComponent }
];
exports.routing = router_1.RouterModule.forRoot(appRoutes);
//# sourceMappingURL=app.routing.js.map