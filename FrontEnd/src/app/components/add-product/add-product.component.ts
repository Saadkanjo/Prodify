import { Component, OnInit } from '@angular/core'; // Import OnInit
import { Router, RouterLink } from '@angular/router';
import { HttpService } from '../../service/http.service'; // Import your HttpService
import { CommonModule } from '@angular/common'; // Import CommonModule
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../service/auth.service';
declare var bootstrap: any;
@Component({
  selector: 'app-add-product',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit { // Implement OnInit
  newProduct = { id: 0, name: '', price: 0, description: '' }; // Include id in the product model
  isEdit: boolean = false; // Flag to check if editing
  isAuthenticated: boolean = false;

  constructor(private httpService: HttpService, private router: Router, private auth: AuthService) {}

  ngOnInit() {
    this.isAuthenticated = this.auth.isAuthenticated();
    const productId = this.router.routerState.snapshot.root.firstChild?.params['id']; // Get the product ID from the route
    if (productId) {
      this.isEdit = true; // Set edit flag
      this.httpService.getProductById(+productId).subscribe(data => {
        this.newProduct = data; // Populate newProduct with fetched data
      });
    }
  }

  addProduct() {
    if (!this.isAuthenticated) {
      const loginAlertModal = new bootstrap.Modal(
        document.getElementById('loginAlertModal')
      );
      loginAlertModal.show();
      return;
    }
    if (this.isEdit) {
      // If editing, call the editProduct method
      this.httpService.editProduct(this.newProduct.id, this.newProduct).subscribe(
        res => {
          console.log('Product edited successfully', res);
          this.router.navigate(['/products']); // Navigate back to the product list page
        },
        error => {
          console.error('Error editing product:', error);
        }
      );
    } else {
      // If adding, call the addProduct method
      this.httpService.addProduct(this.newProduct).subscribe(
        res => {
          console.log('Product added successfully', res);
          this.router.navigate(['/products']); // Navigate back to the product list page
        },
        error => {
          console.error('Error adding product:', error);
        }
      );
    }
  }
}
