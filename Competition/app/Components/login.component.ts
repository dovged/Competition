import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../Service/user.service';
import { ILogin } from '../Model/login';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
    moduleId: module.id,
    templateUrl: 'login.component.html'
})

export class LoginComponent implements OnInit {
    model: any = {};
    loading = false;
    msg: string;
    userFrm: FormGroup;
    loginUser: ILogin;

    constructor(
        private router: Router,
        private _userService: UserService,
        private fb: FormBuilder) { }

    ngOnInit() {
        this.userFrm = this.fb.group({
            userName: ['', Validators.required],
            password: ['', Validators.required]
        });
    }

    login() {
        this.loading = true;
        this._userService.login(this.model.username, this.model.password)
            .subscribe(
            data => {
                this.router.navigate(['/home']);
            },
            error => {
                this.msg = error;
                this.loading = false;
            });
    }
}
