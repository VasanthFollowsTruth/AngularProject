import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { LoginRequest } from '../../models/auth/login-request.model';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html'
})
export class LoginComponent {

  model: LoginRequest = {
    email: '',
    password: ''
  };

  error = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  login(): void {

    this.authService.login(this.model).subscribe({
      next: () => {
        this.router.navigate(['/contacts']);
      },
      error: () => {
        this.error = 'Invalid credentials';
      }
    });
  }
}
