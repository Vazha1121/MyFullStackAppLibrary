import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../Services/api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-product-type',
  imports: [],
  templateUrl: './product-type.component.html',
  styleUrl: './product-type.component.scss',
})
export class ProductTypeComponent implements OnInit {
  constructor(
    public api: ApiService,
    public route: ActivatedRoute,
    public cookie: CookieService,
    public router: Router,
  ) {}

  productType!: any;
  ngOnInit(): void {
    this.productType = +this.route.paramMap.subscribe((params) => {
      const type = params.get('productType');
      if (type) {
        this.productType = +type;
        this.getProductType();
      }
    });
  }
  public prodTypeArray: any[] =[];
  public prodType: any;


  getProductType() {
    this.api.getProductType(this.productType).subscribe({
      next: (data: any) => {
        this.prodTypeArray = data.data;
        this.prodType = this.prodTypeArray[0].productType
      },
    });
  }
  goDetail(productId: any) {
    this.router.navigate(['/']);
    this.router.navigate(['/prodDetail', productId]);
  }
  deleteProduct(productId: number) {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.cookie.get('adminToken')}`,
    });

    this.api.deleteProduct(productId, headers).subscribe({
      next: (data: any) => {
      },
    });
  }
}
