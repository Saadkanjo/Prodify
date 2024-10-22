import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  
apiurl = 'http://localhost:5220'
  constructor(private http:HttpClient){}
  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    }

    getproducts(){
    return this.http.get(this.apiurl+'/api/Product')
    }
    addProduct(product: any): Observable<any> {

      return this.http.post<any>(this.apiurl + '/api/Product', product,{headers: this.getHeaders()});
    }
    getProductById(productId: number): Observable<any> {
      return this.http.get<any>(this.apiurl + '/api/Product/' + productId);
    }
    editProduct(productId: number,product:any) {
       //Logic to edit the product
     return this.http.put<any>(this.apiurl + '/api/Product/' + productId, product,{headers: this.getHeaders()});
      // You can redirect to an edit page or open a modal
    }
    deleteProduct(productId: number): Observable<any> {
      return this.http.delete<any>(this.apiurl + '/api/Product/' + productId,{headers: this.getHeaders()});
    }
  
    
    
    
  }
  
