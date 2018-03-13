import { Component, OnInit, ViewChild } from '@angular/core';
import { AccountService } from '../Service/account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { IUser } from '../Model/user';
import { ITeam } from '../Model/team';
import { DBOperation } from '../Shared/enum';
import { Observable } from 'rxjs/Rx';
import { Global } from '../Shared/global';

@Component({
    templateUrl: 'app/Components/accounts.component.html',
    styleUrls: ['app/Components/user.component.css']

})

export class AccountComponent implements OnInit {

    @ViewChild('modal') modal: ModalComponent;
    user: IUser;
    teams: ITeam[];
    team: ITeam;
    msg: string;
    indLoading: boolean = false;
    accountFrm: FormGroup;
    dbops: DBOperation;
    modalTitle: string;
    modalBtnTitle: string;
    Id: number;

    constructor(private fb: FormBuilder, private _accountService: AccountService) { }

    ngOnInit(): void {
        this.accountFrm = this.fb.group({
            Id: [''],
            Name: ['', Validators.required],
            Points: ['', Validators.required]
        });
        this.LoadAccount();
    }

    LoadAccount(): void {
        this.indLoading = true;
        this.Id = 3;
        this._accountService.getid(Global.BASE_USER_ENDPOINT, this.Id)
            .subscribe(user => { this.user = user; this.indLoading = false; },
            error => this.msg = <any>error);
    }
    /*
    addPenalty() {
        this.dbops = DBOperation.create;
        this.SetControlsState(true);
        this.modalTitle = "Add New User";
        this.modalBtnTitle = "Add";
        this.penaltyFrm.reset();
        this.modal.open();
    }

    editPenalty(id: number) {
        this.dbops = DBOperation.update;
        this.SetControlsState(true);
        this.modalTitle = "Edit User";
        this.modalBtnTitle = "Update";
        this.penalty = this.penalties.filter(x => x.Id == id)[0];
        this.penaltyFrm.setValue(this.penalty);
        this.modal.open();
    }

    deletePenalty(id: number) {
        this.dbops = DBOperation.delete;
        this.SetControlsState(false);
        this.modalTitle = "Confirm to Delete?";
        this.modalBtnTitle = "Delete";
        this.penalty = this.penalties.filter(x => x.Id == id)[0];
        this.penaltyFrm.setValue(this.penalty);
        this.modal.open();
    }

    onSubmit(formData: any) {
        this.msg = "";

        switch (this.dbops) {
            case DBOperation.create:
                this._penaltyService.post(Global.BASE_PENALTY_ENDPOINT, formData._value).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully added.";
                            this.LoadPenalty();
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
                this._penaltyService.put(Global.BASE_PENALTY_ENDPOINT, formData._value.Id, formData._value).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully updated.";
                            this.LoadPenalty();
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
                this._penaltyService.delete(Global.BASE_PENALTY_ENDPOINT, formData._value.Id).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully deleted.";
                            this.LoadPenalty();
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
        isEnable ? this.penaltyFrm.enable() : this.penaltyFrm.disable();
    }*/
}