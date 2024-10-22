import { Routes } from '@angular/router';
import { SignupComponent } from './components/signup/signup.component';
import { LoginComponent } from './components/login/login.component';
import { ProductsComponent } from './components/products/products.component';
import { FeedbacksComponent } from './components/feedbacks/feedbacks.component';
import { ProtofilesComponent } from './components/protofiles/protofiles.component';
import { AddProductComponent } from './components/add-product/add-product.component'; // Import AddProductComponent

export const routes: Routes = [
  { path: "", component: ProductsComponent },
  { path: "login", component: LoginComponent },
  { path: "signup", component: SignupComponent },
  { path: "products", component: ProductsComponent }, // Ensure path is lowercase for consistency
  { path: "feedbacks", component: FeedbacksComponent },
  { path: "protofiles", component: ProtofilesComponent },
  { path: "add-product", component: AddProductComponent } ,// Add this line for AddProductComponent
  { path: "add-product/:id", component: AddProductComponent }
];
