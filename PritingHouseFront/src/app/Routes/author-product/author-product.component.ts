import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../Services/api.service';
import { CookieService } from 'ngx-cookie-service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-author-product',
  imports: [],
  templateUrl: './author-product.component.html',
  styleUrl: './author-product.component.scss',
})
export class AuthorProductComponent implements OnInit {
  constructor(
    public api: ApiService,
    public cookie: CookieService,
    public route: ActivatedRoute,
    public router: Router
  ) {}

  productAuthor!: any;
  ngOnInit(): void {
    this.productAuthor = +this.route.paramMap.subscribe((params) => {
      const type = params.get('authorId');
      if (type) {
        this.productAuthor = +type;
        this.getAuthorProducts();
      }
    });
  }
  public authorProd: any;
  getAuthorProducts() {
    this.api.getAuthorProduct(this.productAuthor).subscribe({
      next: (data: any) => {
        this.authorProd = data.data;
      },
    });
  }

  goDetail(productId: any) {
    this.router.navigate(['/']);
    this.router.navigate(['/prodDetail', productId]);
  }
  deleteProduct(productId: number) {
    const headers = new HttpHeaders
    ({
      Authorization: `Bearer ${this.cookie.get('adminToken')}`,
    });

    this.api.deleteProduct(productId, headers).subscribe({
      next: (data: any) => {
      },
    });
  }
}
