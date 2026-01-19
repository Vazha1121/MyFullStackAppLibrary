import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../Services/api.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-loader',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './loader.component.html',
  styleUrl: './loader.component.scss',
})
export class LoaderComponent implements OnInit {
  constructor(public api: ApiService) {}

  public isLoading: any;
  ngOnInit(): void {
    this.api.loader.subscribe((data: any) => {
      this.isLoading = data;
    });
  }
}
