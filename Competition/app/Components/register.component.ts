import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../Service/user.service';
import { Global } from '../Shared/global';
import { IRegister } from '../Model/register';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
    moduleId: module.id,
    templateUrl: 'register.component.html'
})

export class RegisterComponent implements OnInit{
    model: any = {};
    loading = false;
    msg: string;
    userFrm: FormGroup;
    registerUser: IRegister;

    constructor(
        private _router: Router,
        private _userService: UserService,
        private fb: FormBuilder) { }

    ngOnInit() {
        this.userFrm = this.fb.group({
            UserName: ['', Validators.required],
            Password: ['', Validators.required],
            ComfirmPassword: ['', Validators.required]
        });
    }

    register() {
        this.loading = true;
        this._userService.Register(Global.BASE_REGISTER_ENDPOINT, this.model)
            .subscribe(
            data => {
                this.msg = "Registration successful";
                this._router.navigate(['/login']);
            },
            error => {
                this.msg = error;
                this.loading = false;
            });
    }
}