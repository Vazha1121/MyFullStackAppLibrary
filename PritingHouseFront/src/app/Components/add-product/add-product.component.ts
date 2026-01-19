import { Component, EventEmitter, Output } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { ApiService } from '../../Services/api.service';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-add-product',
  imports: [ReactiveFormsModule],
  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.scss',
})
export class AddProductComponent {
  @Output() close = new EventEmitter<void>();
  productForm: FormGroup;
  constructor(
    public cookie: CookieService,
    public api: ApiService,
    private fb: FormBuilder,
  ) {
    this.productForm = this.fb.group({
      ProductName: ['', Validators.required],
      Desctription: ['', Validators.required],
      ProductType: ['', Validators.required],
      ISBN: ['', [Validators.required, Validators.pattern(/^\d{13}$/)]],
      publisherId: [1, Validators.required],
      Image: [null, Validators.required],
    });
  }

  public selectedFile!: File;
  onFileSelected(event: any) {
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
      this.productForm.patchValue({ Image: this.selectedFile });
    }
  }
  AddProduct() {
    console.log(this.productForm.value);
    const formData = new FormData();
    formData.append('ProductName', this.productForm.value.ProductName);
    formData.append('Desctription', this.productForm.value.Desctription);
    formData.append('ProductType', this.productForm.value.ProductType);
    formData.append('ISBN', this.productForm.value.ISBN);
    formData.append(
      'publisherId',
      this.productForm.value.publisherId.toString(),
    );

    if (this.selectedFile) {
      formData.append('Image', this.selectedFile);
    }

    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.cookie.get('adminToken')}`,
    });

    this.api.addProduct(formData, headers).subscribe({
      next: (data: any) => {
        if (data.isSuccess == true) {
          this.close.emit();
          window.location.href = window.location.href;
        }
      },
      error: (err) => console.error('Error', err),
    });
  }

  closePopUp(){
    this.close.emit()
  }
}
