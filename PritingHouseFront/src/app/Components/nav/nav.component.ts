import { Component, OnInit } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { ApiService } from '../../Services/api.service';
import { RegisterComponent } from '../register/register.component';
import { LoginComponent } from '../login/login.component';
import { CookieService } from 'ngx-cookie-service';
import { Router, RouterModule } from '@angular/router';
@Component({
  selector: 'app-nav',
  imports: [MatSidenavModule, RegisterComponent, LoginComponent, RouterModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.scss',
})
export class NavComponent implements OnInit {
  constructor(
    public apiService: ApiService,
    public cookie: CookieService,
    public router: Router,
  ) {}

  ngOnInit(): void {
    this.cookie.get('adminToken');
    this.cookie.get('userToken');
    this.cookie.get('managerToken');
  }
  showRegister: boolean = false;
  showLogin: boolean = false;
  RegisterPopUp() {
    this.showRegister = !this.showRegister;
  }
  LoginPopUp() {
    this.showLogin = !this.showLogin;
  }
  LogOut() {
    this.cookie.deleteAll('userToken');
    this.cookie.deleteAll('adminToken');
    this.cookie.deleteAll('managerToken');
  }
  CloseRegisterPopUp() {
    this.showRegister = false;
  }
  CloseLoginPopUp() {
    this.showLogin = false;
  }

  /* Go product type */

  goProdType(productType: any) {
    this.router.navigate(['/product', productType]);
  }
  goBook(productType: any) {
    this.router.navigate(['/product', productType]);
  }
  goArticle(productType: any) {
    this.router.navigate(['/product', productType]);
  }
  goElResource(productType: any) {
    this.router.navigate(['/product', productType]);
  }

  /* search */
  searchedData: any;
  onSearch(event: any) {
    const value = event.target.value;

    if (!value.trim()) {
      this.searchedData = [];
      return;
    }

    this.apiService.productSearch(value).subscribe({
      next: (data: any) => {
        this.searchedData = data.data;
        this.apiService.setSearchResult(data.data);
        this.router.navigate(['/']);
      },
    });
  }
}
