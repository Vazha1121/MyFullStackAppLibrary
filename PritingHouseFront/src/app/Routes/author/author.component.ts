import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../Services/api.service';
import { CookieService } from 'ngx-cookie-service';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { AddAuthorComponent } from '../../Components/add-author/add-author.component';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
@Component({
  selector: 'app-author',
  imports: [
    MatPaginatorModule,
    AddAuthorComponent,
    CommonModule,
    MatTableModule,
    MatButtonModule,
  ],
  templateUrl: './author.component.html',
  styleUrl: './author.component.scss',
})
export class AuthorComponent implements OnInit {
  constructor(
    public api: ApiService,
    public cookie: CookieService,
    public router: Router,
  ) {}

  ngOnInit(): void {
    this.getAuthors(this.pageIndex, this.pageSize);
  }
  public authorData: any[] = [];
  totalCount = 0;
  pageIndex = 0;
  pageSize = 10;
  authorId!: number;

  public dataSource = new MatTableDataSource<any>();

  getAuthors(pageIndex: number, pageSize: number) {
    this.api.getAuthor(pageIndex + 1, pageSize).subscribe({
      next: (data: any) => {
        this.dataSource.data = data.data;

        this.authorData = data.data;
        this.authorId = data.data.authorId;
      },
    });
  }

  deleteAuthor(authorID: any) {
    window.location.href = window.location.href;
    this.api.deleteAuthor(authorID).subscribe({
      next: (data: any) => {
      },
      error: (errData: any) => {},
    });
  }
  onPageChange(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;

    this.getAuthors(this.pageIndex, this.pageSize);
  }

  showAddAuthorPopUp: boolean = false;
  AddAuthorPopUp() {
    this.showAddAuthorPopUp = !this.showAddAuthorPopUp;
  }
  CloseAuthorPopUp() {
    this.showAddAuthorPopUp = false;
  }

  displayedColumns: string[] = ['firstName', 'lastName', 'actions'];

  goBooksPage(authorId: any) {
    this.router.navigate(['/author', authorId, 'products']);
  }
}
