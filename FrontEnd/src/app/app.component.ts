import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { AuthService } from './service/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'prodify';
    isloggedin=false;
  constructor (private auth : AuthService) {
  }
  ngOnInit(){
     this.isloggedin=this.auth.isAuthenticated()
  }
  logout(){
    return this.auth.logout();
    this.isloggedin=false;
   }
}
