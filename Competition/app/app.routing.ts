import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UserComponent } from './components/user.component';
import { HomeComponent } from './components/home.component';
import { CalendarForCompComponent } from './components/calendarForComp.component';
import { CalendarForOrgComponent } from './components/calendarForOrg.component';
import { PenaltyComponent } from './components/penalty.component';
import { ResultsComponent } from './components/results.component';
import { RouteComponent } from './components/route.component';
import { CompTeamsComponent } from './components/compteams.component';
import { LoginComponent } from './components/login.component';
import { RegisterComponent } from './components/register.component';

const appRoutes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'admin', component: UserComponent },
    { path: 'calendar', component: CalendarForCompComponent },
    { path: 'penalty', component: PenaltyComponent },
    { path: 'results', component: ResultsComponent },
    { path: 'routes', component: RouteComponent },
    { path: 'calendarORG', component: CalendarForOrgComponent },
    { path: 'compteams', component: CompTeamsComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent }
];

export const routing: ModuleWithProviders =
    RouterModule.forRoot(appRoutes);