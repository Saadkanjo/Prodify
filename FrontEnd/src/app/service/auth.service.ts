import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  apiUrl = "http://localhost:5220";
  http = inject(HttpClient);

  login(user:any): Observable<any> {
    return this.http.post<any>(this.apiUrl + '/api/Account/login', user);
  }
 
  RegisterUser(User:any): Observable<any> {
    return this.http.post<any>(this.apiUrl + '/api/Account/register', User );
  }

  logout() {
    localStorage.removeItem('authToken');
    localStorage.removeItem('userInfo');
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem('authToken');
    return token != null;
  }

  getToken(){
    return localStorage.getItem('authToken');
  }

  getUserId() {
    const userInfo = localStorage.getItem('userInfo');
    if (userInfo) {
      const parsedUserInfo = JSON.parse(userInfo);
      return parsedUserInfo.userId; 
    }
    return null;
  }
  
  getUserName(){
    const userInfo = localStorage.getItem('userInfo');
    if (userInfo){
      const parsedUserInfo = JSON.parse(userInfo);
      return parsedUserInfo.username 
    }
    return null
  }
}