import { Component, inject } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../service/auth.service';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink,FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  formBuilder = inject(FormBuilder);
  constructor(private auth: AuthService, private router: Router) {}
  user = this.formBuilder.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
  });

  login() {
    if (this.user.valid) {
      const user = this.user.value;
      this.auth.login(user).subscribe((response) => {
        localStorage.setItem('authToken', response.token);

        localStorage.setItem(
          'userInfo',
          JSON.stringify({
            username: response.username,
            email: response.email,
          })
        );
        this.router.navigateByUrl('/products');
      });
    }
  }
}
