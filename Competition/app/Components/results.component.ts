import { Component, OnInit, ViewChild } from '@angular/core';
import { ResultsService } from '../Service/results.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { IResults } from '../Model/results';
import { DBOperation } from '../Shared/enum';
import { Observable } from 'rxjs/Rx';
import { Global } from '../Shared/global';
import { DataService } from '../Shared/DataService';
import { Router } from '@angular/router';

@Component({
    templateUrl: 'app/Components/results.component.html'
})

export class ResultsComponent implements OnInit {

    @ViewChild('modal') modal: ModalComponent;
    results: IResults[];
    msg: string;
    indLoading: boolean = false;
    modalTitle: string;
    modalBtnTitle: string;
    Id: number;
    userFrm: FormGroup;

    constructor(private fb: FormBuilder, private _resultsService: ResultsService, private _dataService: DataService, private _router: Router) { }

    ngOnInit(): void {
        this.LoadResults();
    }

    LoadResults(): void {
        this.indLoading = true;
        this.Id = this._dataService.getData();
        this._resultsService.get(Global.BASE_RESULTS_ENDPOINT, this.Id)
            .subscribe(results => { this.results = results; this.indLoading = false; },
            error => this.msg = <any>error);
    }

    SetControlsState(isEnable: boolean) {
        isEnable ? this.userFrm.enable() : this.userFrm.disable();
    }
}