import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(public http: HttpClient) {}

  /* search services */
  private searchResultSource = new BehaviorSubject<any[]>([]);
  searchResult$ = this.searchResultSource.asObservable();

  setSearchResult(data: any[]) {
    this.searchResultSource.next(data);
  }
  public emailSource = new BehaviorSubject<string>('');

  /* loader */
  public loader: BehaviorSubject<any> = new BehaviorSubject(false);
  loading$ = this.loader.asObservable();

  private counter = 0;
  /* ამას ვამატებ რადგან ლოადერი წამებით ჩანს და ui ზე გამოჩენას ვერ ასწრებს */
  private minTime = 400;
  private startTime = 0;
  startLoader() {
    if (this.counter === 0) {
      this.startTime = Date.now();
      this.loader.next(true);
    }
    this.counter++;
  }
  stopLoader() {
    this.counter--;

    if (this.counter === 0) {
      const elapsed = Date.now() - this.startTime;
      const delay = Math.max(this.minTime - elapsed, 0);

      setTimeout(() => {
        this.loader.next(false);
      }, delay);
    }
  }

  /* authorization */

  Register(header: any) {
    return this.http.post(`https://localhost:7197/api/auth/register`, header);
  }
  Login(header: any) {
    return this.http.post(`https://localhost:7197/api/auth/login`, header);
  }
  GetProduct() {
    return this.http.get(`https://localhost:7197/api/products/get-all-product`);
  }

  /* authors */
  getAuthor(pageNumber: any, pageSize: any) {
    return this.http.get(
      `https://localhost:7197/api/authors/GetAuthorPaged?pageNumber=${pageNumber}&pageSize=${pageSize}`,
    );
  }
  addAuthor(body: any, header: any) {
    return this.http.post(
      `https://localhost:7197/api/authors/add-author`,
      body,
      { headers: header },
    );
  }

  deleteAuthor(authorID: any) {
    return this.http.delete(
      `https://localhost:7197/api/authors/delete-author/${authorID}`,
    );
  }
  /* users */

  getUserWithId(userId: any) {
    return this.http.get(
      `https://localhost:7197/api/admin/users/user/5/${userId}`,
    );
  }

  getAllUser() {
    return this.http.get(
      `https://localhost:7197/api/admin/users/get-all-users`,
    );
  }

  getUserByEmail(email: any) {
    return this.http.get(
      `https://localhost:7197/api/admin/users/get-user-email?email=${encodeURIComponent(email)}`,
    );
  }

  deleteUser(userId: number) {
    return this.http.delete(
      `https://localhost:7197/api/admin/users/delete-user/${userId}`,
    );
  }

  editUser(userId: number, header: any) {
    return this.http.put(
      `https://localhost:7197/api/admin/users/update-user/${userId}`,
      header,
    );
  }
  /* products */

  addProduct(body: any, header: any) {
    return this.http.post(
      `https://localhost:7197/api/products/create-product`,
      body,
      { headers: header },
    );
  }

  deleteProduct(authorID: number, header: any) {
    return this.http.delete(
      `https://localhost:7197/api/products/delete-item/${authorID}`,
      { headers: header },
    );
  }

  getProductById(productId: number) {
    return this.http.get(
      `https://localhost:7197/api/products/item/${productId}`,
    );
  }

  getProductType(productType: any) {
    return this.http.get(
      `https://localhost:7197/api/products/getProductType/${productType}`,
    );
  }

  getAuthorProduct(authorId: any) {
    return this.http.get(
      `https://localhost:7197/api/products/author/${authorId}/products`,
    );
  }

  productSearch(query: string) {
    return this.http.get(`https://localhost:7197/api/products/searchProduct`, {
      params: { query: query },
    });
  }
}
