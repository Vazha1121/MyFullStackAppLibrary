import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ApiService } from '../../Services/api.service';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  @Output() close = new EventEmitter<void>();

  constructor(
    public api: ApiService,
    public cookie: CookieService,
  ) {}

  public loginIn: FormGroup = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
  });
  email: string = '';
  LoginIn() {
    this.api.Login(this.loginIn.value).subscribe({
      next: (data: any) => {
        console.log(data);
        if (this.loginIn.value.email == 'vajikoiakobashvili9@gmail.com') {
          this.cookie.set('adminToken', data.data);
          this.cookie.set('userEmail', this.loginIn.value.email);
          this.email = this.loginIn.value.email;
          this.api.emailSource.next(this.email);
        } else if (this.loginIn.value.email == 'kapanadze2000@gmail.com') {
          this.cookie.set('managerToken', data.data);
          this.cookie.set('userEmail', this.loginIn.value.email);
        } else {
          this.cookie.set('userToken', data.data);
          this.cookie.set('userEmail', this.loginIn.value.email);
        }
        if (
          this.cookie.get('adminToken') ||
          this.cookie.get('managerToken') ||
          this.cookie.get('userToken')
        ) {
          this.close.emit();
        }
      },
    });
  }
  LogOut() {
    if (this.cookie.get('adminToken')) {
      this.cookie.delete('adminToken');
    } else if (this.cookie.get('managerToken')) {
      this.cookie.delete('managerToken');
    } else {
      this.cookie.delete('userToken');
    }
  }
  closeLoginPopup() {
    this.close.emit();
  }
}
