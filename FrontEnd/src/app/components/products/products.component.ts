import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router'; // Import Router
import { HttpService } from '../../service/http.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../service/auth.service';
declare var bootstrap: any;

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})
export class ProductsComponent {
  productlist: any = [];
  isAuthenticated: boolean = false;

  constructor(
    private httpservice: HttpService,
    private router: Router,
    private auth: AuthService
  ) {} // Inject Router

  ngOnInit() {
    this.isAuthenticated = this.auth.isAuthenticated();
    this.httpservice.getproducts().subscribe((res) => {
      this.productlist = res;
      console.log(this.productlist);
    });
  }

  // Navigate to Add Product page
  goToAddProduct() {
    this.router.navigate(['/add-product']); // Route to the add-product page
  }

  // Delete product by ID
  deleteProduct(productId: number) {
    if (!this.isAuthenticated) {
      const loginAlertModal = new bootstrap.Modal(
        document.getElementById('loginAlertModal')
      );
      loginAlertModal.show();
      return;
    }
    this.httpservice.deleteProduct(productId).subscribe(
      () => {
        // After successfully deleting the product, remove it from the product list
        this.productlist = this.productlist.filter(
          (product: any) => product.id !== productId
        );
        console.log('Deleted product with ID:', productId);
      },
      (error) => {
        console.error('Error deleting product:', error);
      }
    );
  }

  // Placeholder for edit functionality

  // Navigate to Add Product page with product ID for editing
  editProduct(productId: number) {
    this.router.navigate(['/add-product', productId]); // Pass product ID as a route parameter
  }
}
