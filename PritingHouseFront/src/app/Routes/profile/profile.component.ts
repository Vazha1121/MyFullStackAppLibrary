import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../Services/api.service';
import { CookieService } from 'ngx-cookie-service';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-profile',
  imports: [],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
})
export class ProfileComponent implements OnInit {
  constructor(
    public api: ApiService,
    public cookie: CookieService,
  ) {}

  ngOnInit(): void {
    this.api.emailSource.subscribe((emailData) => {
      this.email = emailData;
      console.log(this.email);
    });
    this.userByEmail();
  }
  private email: string = '';
  public userInfo: any = {};
  private userId!: number;

  userByEmail() {
    this.email = this.cookie.get('userEmail');
    this.api.getUserByEmail(this.email).subscribe({
      next: (data: any) => {
        this.userInfo = data.data;
        console.log(this.userInfo);

        this.userId = this.userInfo.userId;
        console.log(this.userInfo.userId);
      },
    });
  }

  deleteUser() {
    this.api.deleteUser(this.userId).subscribe({
      next: (data: any) => {
        console.log(data);
      },
    });
  }

  public editUser: FormGroup = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    isAdmin: new FormControl(''),
  });

  editProfile() {
    this.api.editUser(this.userId, this.editUser.value).subscribe({
      next: (data: any) => {
        console.log(data);
      },
      error: (errData: any) => {
        console.log(errData);
      },
    });
  }
}
