import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { HttpHeaders } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { ApiService } from '../../Services/api.service';

@Component({
  selector: 'app-add-author',
  imports: [ReactiveFormsModule],
  templateUrl: './add-author.component.html',
  styleUrl: './add-author.component.scss',
})
export class AddAuthorComponent {
  @Output() close = new EventEmitter<void>();

  constructor(public api: ApiService, public cookie: CookieService) {}
  public addAuthorForm: FormGroup = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    gender: new FormControl(''),
    personalNumber: new FormControl(''),
    birthDate: new FormControl(''),
    countryId: new FormControl(''),
    cityId: new FormControl(''),
    phoneNumber: new FormControl(''),
    email: new FormControl(''),
  });

  AddAuthor() {
    const f = this.addAuthorForm.value;

    const body = {
      authorId: 0,
      firstName: f.firstName,
      lastName: f.lastName,
      gender: Number(f.gender),
      personalNumber: f.personalNumber,
      birthDate: new Date(f.birthDate).toISOString(),
      countryId: Number(f.countryId),
      cityId: Number(f.cityId),
      phoneNumber: f.phoneNumber,
      email: f.email,
    };

    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.cookie.get('adminToken')}`,
      'Content-Type': 'application/json',
    });
    this.api.addAuthor(body, headers).subscribe({
      next: (data: any) => {
        console.log(data);
        if (data.isSuccess == true) {
          this.close.emit();
          window.location.href = window.location.href
        }
      },
    });
  }
  closePopup() {
    this.close.emit();
  }
}
