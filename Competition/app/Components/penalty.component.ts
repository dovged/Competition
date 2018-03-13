import { Component, OnInit, ViewChild } from '@angular/core';
import { PenaltyService } from '../Service/penalty.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { IPenalty } from '../Model/penalty';
import { DBOperation } from '../Shared/enum';
import { Observable } from 'rxjs/Rx';
import { Global } from '../Shared/global';

@Component({
    templateUrl: 'app/Components/penalty.component.html',
    styleUrls: ['app/Components/user.component.css']

})

export class PenaltyComponent implements OnInit {

    @ViewChild('modal') modal: ModalComponent;
    penalties: IPenalty[];
    penalty: IPenalty;
    msg: string;
    indLoading: boolean = false;
    penaltyFrm: FormGroup;
    dbops: DBOperation;
    modalTitle: string;
    modalBtnTitle: string;

    constructor(private fb: FormBuilder, private _penaltyService: PenaltyService) { }

    ngOnInit(): void {
        this.penaltyFrm = this.fb.group({
            Id: [''],
            Name: ['', Validators.required],
            Points: ['', Validators.required]           
        });
        this.LoadPenalty();
    }

    LoadPenalty(): void {
        this.indLoading = true;
        this._penaltyService.get(Global.BASE_PENALTY_ENDPOINT)
            .subscribe(penalties => { this.penalties = penalties; this.indLoading = false; },
            error => this.msg = <any>error);
    }

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
    }
}