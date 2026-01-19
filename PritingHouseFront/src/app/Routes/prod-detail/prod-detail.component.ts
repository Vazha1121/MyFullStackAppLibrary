import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../Services/api.service';

@Component({
  selector: 'app-prod-detail',
  imports: [],
  templateUrl: './prod-detail.component.html',
  styleUrl: './prod-detail.component.scss',
})
export class ProdDetailComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    public api: ApiService,
  ) {}

  productId!: number;
  ngOnInit(): void {
    this.productId = +this.route.snapshot.paramMap.get('id')!;
    this.getProductDetail();
  }

  public productDetail: any = {};
  getProductDetail() {
    this.api.getProductById(this.productId).subscribe({
      next: (data: any) => {
        this.productDetail = data.data;
      },
    });
  }
}
