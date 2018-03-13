import { Component, OnInit, ViewChild } from '@angular/core';
import { CalendarService } from '../Service/calendor.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { ICompetition } from '../Model/competition';
import { DBOperation } from '../Shared/enum';
import { Observable } from 'rxjs/Rx';
import { Global } from '../Shared/global';
import { DataService } from '../Shared/DataService';
import { Router } from '@angular/router';

@Component({
    templateUrl: 'app/Components/calendar2.component.html',
    styleUrls: ['app/Components/calendar.component.css']

})

export class Calendar2Component implements OnInit {

    @ViewChild('modal') modal: ModalComponent;
    comps: ICompetition[];
    comp: ICompetition;
    msg: string;
    indLoading: boolean = false;
    compFrm: FormGroup;
    dbops: DBOperation;
    modalTitle: string;
    modalBtnTitle: string;

    constructor(private fb: FormBuilder, private _calendarService: CalendarService, private _dataService: DataService, private _router: Router) { }

    ngOnInit(): void {
        this.LoadCompetitions();
    }

    LoadCompetitions(): void {
        this.indLoading = true;
        this._calendarService.get(Global.BASE_COMPETITION_ENDPOINT)
            .subscribe(comps => { this.comps = comps; this.indLoading = false; },
            error => this.msg = <any>error);
    }

    getRoutes(id: number) {
        this._dataService.saveData(id);
        this._router.navigate(['/routes']);
    }
    getTeams(id: number) {
        this._dataService.saveData(id);
        this._router.navigate(['/compteams']);
    }
    SetControlsState(isEnable: boolean) {
        isEnable ? this.compFrm.enable() : this.compFrm.disable();
    }
}