import { Component, OnInit, ViewChild } from '@angular/core';
import { ApiService } from '../../Services/api.service';
import { CookieService } from 'ngx-cookie-service';
import { AddProductComponent } from '../../Components/add-product/add-product.component';
import { HttpHeaders } from '@angular/common/http';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
@Component({
  selector: 'app-home',
  imports: [AddProductComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit{
  @ViewChild(MatSort) sort!: MatSort;
  dataSource!: MatTableDataSource<any>;

  constructor(
    public api: ApiService,
    public cookie: CookieService,
    private router: Router,
  ) {
    this.GetProduct();
  }


  ngOnInit(): void {
    this.api.searchResult$.subscribe(data => {
      if(data && data.length > 0){
        this.productData = data;
      }
    })
  }
  public productData: any[] = [];
  GetProduct() {
    this.api.GetProduct().subscribe({
      next: (data: any) => {
        this.productData.slice(0,8);
        this.productData = data.data;

        this.dataSource = new MatTableDataSource(this.productData);

        this.dataSource.sort = this.sort;
      },
    });
  }

  deleteProduct(productId: number) {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.cookie.get('adminToken')}`,
    });
    console.log(productId);

    this.api.deleteProduct(productId, headers).subscribe({
      next: (data: any) => {
      },
    });
  }

  /* DetailPage */

  goDetail(productId: any) {
    this.router.navigate(['/prodDetail', productId])
    console.log(productId);
  }

  showProductPopUp: boolean = false;
  addProductPopUp() {
    this.showProductPopUp = !this.showProductPopUp;
  }
  CloseProductPopUp() {
    this.showProductPopUp = false;
  }
}
