import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AccountsService {

  constructor(private http: HttpClient) { }

  SignUp(){
    
  }

  // SignIn(username: string, password: string): User{
  //   this.http.get<User>(baseUrl + 'weatherforecast').subscribe(result => {
  //     this.forecasts = result;
  //   }, error => console.error(error));

  // }
}
