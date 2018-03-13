import { NgModule } from '@angular/core';
import { APP_BASE_HREF } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { Ng2Bs3ModalModule } from 'ng2-bs3-modal/ng2-bs3-modal';
import { HttpModule } from '@angular/http';
import { routing } from './app.routing';

/** Components*/
import { UserComponent } from './components/user.component';
import { HomeComponent } from './components/home.component';
import { FooterComponent } from './footer/footer.component';
import { CalendarComponent } from './components/calendar.component';
import { Calendar2Component } from './components/calendar2.component';
import { PenaltyComponent } from './components/penalty.component';
import { ResultsComponent } from './components/results.component';
import { RouteComponent } from './components/route.component';
import { CompTeamsComponent } from './components/compteams.component';
import { LoginComponent } from './components/login.component';
import { RegisterComponent} from './components/register.component';

/* Services*/
import { UserService } from './Service/user.service';
import { CalendarService } from './Service/calendor.service';
import { PenaltyService } from './Service/penalty.service';
import { DataService } from './Shared/DataService';
import { ResultsService } from './Service/results.service';
import { RouteService } from './Service/route.service';
import { CompTeamService } from './Service/compTeam.service';

@NgModule({
    imports: [BrowserModule, ReactiveFormsModule, HttpModule, routing, Ng2Bs3ModalModule],
    declarations: [AppComponent, UserComponent, CalendarComponent, PenaltyComponent,
        HomeComponent, FooterComponent, ResultsComponent, RouteComponent,
        Calendar2Component, CompTeamsComponent, LoginComponent, RegisterComponent],
    providers: [{ provide: APP_BASE_HREF, useValue: '/' }, UserService, PenaltyService, CalendarService, DataService, ResultsService, RouteService, CompTeamService],
    bootstrap: [AppComponent]

})
export class AppModule { }