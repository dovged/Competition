import { Component, OnInit, ViewChild } from '@angular/core';
import { RouteService } from '../Service/route.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { IRoute } from '../Model/route';
import { DBOperation } from '../Shared/enum';
import { Observable } from 'rxjs/Rx';
import { Global } from '../Shared/global';
import { DataService } from '../Shared/DataService';
import { Router } from '@angular/router';

@Component({
    templateUrl: 'app/Components/route.component.html',
    styleUrls: ['app/Components/user.component.css']

})

export class RouteComponent implements OnInit {

    @ViewChild('modal') modal: ModalComponent;
    routes: IRoute[];
    route: IRoute;
    msg: string;
    indLoading: boolean = false;
    routeFrm: FormGroup;
    dbops: DBOperation;
    modalTitle: string;
    modalBtnTitle: string;
    Id: number;
    routesAll: IRoute[]

    constructor(private fb: FormBuilder, private _routeService: RouteService, private _dataService: DataService, private _router: Router) { }

    ngOnInit(): void {
        this.routeFrm = this.fb.group({
            Id: [''],
            Name: ['', Validators.required],
            Points: ['', Validators.required],
            Time: ['', Validators.required]
        });
        this.LoadRoute();
    }

    LoadRoute(): void {
        this.indLoading = true;
        this.Id = this._dataService.getData();
        this._routeService.get(Global.BASE_ROUTE_ENDPOINT)
            .subscribe(routes => { this.routes = routes; this.indLoading = false; },
            error => this.msg = <any>error);
        var t = 0;
       /* for (var item of this.routesAll) {
            if (item.CompId == this.Id) {
                this.routes[0] = item;
                t++;
            }
        }*/
    }

    addRoute() {
        this.dbops = DBOperation.create;
        this.SetControlsState(true);
        this.modalTitle = "Add Newr";
        this.modalBtnTitle = "Add";
        this.routeFrm.reset();
        this.modal.open();
    }

    editRoute(id: number) {
        this.dbops = DBOperation.update;
        this.SetControlsState(true);
        this.modalTitle = "Edit User";
        this.modalBtnTitle = "Update";
        this.route = this.routes.filter(x => x.Id == id)[0];
        this.routeFrm.setValue(this.route);
        this.modal.open();
    }

    deleteRoute(id: number) {
        this.dbops = DBOperation.delete;
        this.SetControlsState(false);
        this.modalTitle = "Confirm to Delete?";
        this.modalBtnTitle = "Delete";
        this.route = this.routes.filter(x => x.Id == id)[0];
        this.routeFrm.setValue(this.route);
        this.modal.open();
    }

    onSubmit(formData: any) {
        this.msg = "";

        switch (this.dbops) {
            case DBOperation.create:
                this._routeService.post(Global.BASE_ROUTE_ENDPOINT, formData._value).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully added.";
                            this.LoadRoute();
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
                this._routeService.put(Global.BASE_ROUTE_ENDPOINT, formData._value.Id, formData._value).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully updated.";
                            this.LoadRoute();
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
                this._routeService.delete(Global.BASE_ROUTE_ENDPOINT, formData._value.Id).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully deleted.";
                            this.LoadRoute();
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
        isEnable ? this.routeFrm.enable() : this.routeFrm.disable();
    }
}