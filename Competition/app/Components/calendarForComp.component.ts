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
    templateUrl: 'app/Components/calendarForComp.component.html',
    styleUrls: ['app/Components/calendar.component.css']

})

export class CalendarForCompComponent implements OnInit {

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
        this.compFrm = this.fb.group({
            Id: [''],
            Name: ['', Validators.required],
            Date: ['', Validators.required],
            UserId: ['']
        });
        this.LoadCompetitions();
    }

    LoadCompetitions(): void {
        this.indLoading = true;
        this._calendarService.get(Global.BASE_COMPETITION_ENDPOINT)
            .subscribe(comps => { this.comps = comps; this.indLoading = false; },
            error => this.msg = <any>error);
    }

    registration(id: number, open: boolean) {
        if (open) {
            this.addRegistration(id);
        }
        else {
            // TO DO
        }

    }

    addRegistration(id: number) {
        this.dbops = DBOperation.create;
        this.SetControlsState(true);
        this.modalTitle = "Regsitruotis į varžybas";
        this.modalBtnTitle = "Registruotis";
        this.compFrm.reset();
        this.modal.open();
    }

    getResults(id: number) {
        this._dataService.saveData(id);
        this._router.navigate(['/results']);
    }

    onSubmit(formData: any) {
        this.msg = "";
        switch (this.dbops) {
            case DBOperation.create:
                this._calendarService.post(Global.BASE_COMPETITION_ENDPOINT, formData._value).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Duomenys sėkmingai išsaugoti.";
                            this.LoadCompetitions();
                        }
                        else {
                            this.msg = "There is some issue in saving records, please contact to system administrator!"
                        }

                        this.modal.dismiss();
                    },
                    error => {
                        this.msg = error;
                    }
                );
                break;
            case DBOperation.update:
                this._calendarService.put(Global.BASE_COMPETITION_ENDPOINT, formData._value.Id, formData._value).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully updated.";
                            this.LoadCompetitions();
                        }
                        else {
                            this.msg = "There is some issue in saving records, please contact to system administrator!"
                        }

                        this.modal.dismiss();
                    },
                    error => {
                        this.msg = error;
                    }
                );
                break;
            case DBOperation.delete:
                this._calendarService.delete(Global.BASE_COMPETITION_ENDPOINT, formData._value.Id).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully deleted.";
                            this.LoadCompetitions();
                        }
                        else {
                            this.msg = "There is some issue in saving records, please contact to system administrator!"
                        }

                        this.modal.dismiss();
                    },
                    error => {
                        this.msg = error;
                    }
                );
                break;

        }
    }

    SetControlsState(isEnable: boolean) {
        isEnable ? this.compFrm.enable() : this.compFrm.disable();
    }
}