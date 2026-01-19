import { Routes } from '@angular/router';
import { HomeComponent } from './Routes/home/home.component';
import { ProfileComponent } from './Routes/profile/profile.component';
import { AuthorComponent } from './Routes/author/author.component';
import { ProdDetailComponent } from './Routes/prod-detail/prod-detail.component';
import { LoginComponent } from './Components/login/login.component';
import { ProductTypeComponent } from './Routes/product-type/product-type.component';
import { AuthorProductComponent } from './Routes/author-product/author-product.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'author', component: AuthorComponent },
  { path: 'prodDetail/:id', component: ProdDetailComponent },
  { path: 'login', component: LoginComponent },
  { path: 'product/:productType', component: ProductTypeComponent },
  { path: 'author/:authorId/products', component: AuthorProductComponent },
  { path: '**', redirectTo: '' },
];
