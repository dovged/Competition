import { Component, OnInit, ViewChild } from '@angular/core';
import { CompTeamService } from '../Service/compTeam.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { ICompTeam } from '../Model/compTeam';
import { DBOperation } from '../Shared/enum';
import { Observable } from 'rxjs/Rx';
import { Global } from '../Shared/global';
import { DataService } from '../Shared/DataService';
import { Router } from '@angular/router';

@Component({
    templateUrl: 'app/Components/compteams.component.html',
    styleUrls: ['app/Components/user.component.css']

})

export class CompTeamsComponent implements OnInit {

    @ViewChild('modal') modal: ModalComponent;
    compteams: ICompTeam[];
    compteam: ICompTeam;
    msg: string;
    indLoading: boolean = false;
    userFrm: FormGroup;
    dbops: DBOperation;
    modalTitle: string;
    modalBtnTitle: string;
    Id: number;

    constructor(private fb: FormBuilder, private _userService: CompTeamService, private _dataService: DataService, private _router: Router) { }

    ngOnInit(): void {
        this.LoadCompTeams();
    }

    LoadCompTeams(): void {
        this.indLoading = true;
        this.Id = this._dataService.getData();
        this._userService.get(Global.BASE_COMPTEAM_ENDPOINT, this.Id)
            .subscribe(compteams => { this.compteams = compteams; this.indLoading = false; },
            error => this.msg = <any>error);
    }

    SetControlsState(isEnable: boolean) {
        isEnable ? this.userFrm.enable() : this.userFrm.disable();
    }
}