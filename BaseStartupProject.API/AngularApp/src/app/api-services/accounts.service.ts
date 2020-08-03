import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { HttpClient } from '@angular/common/http';
import { BaseHttpClient } from './base-http-client';
import { GlobalService } from '../global.service';
import { MessageService } from '../message.service';
import { Observable } from 'rxjs';
import { PopupsService } from '../popups.service';
import { SignUpUser } from './../ServiceCommands/SignUpUser'
import { UserLogin } from '../ServiceCommands/UserLogin';
import { UserQuery } from '../ServiceQueries/UserQuery';

@Injectable({
  providedIn: 'root'
})
export class AccountsService extends BaseHttpClient {

  constructor(http: HttpClient, global: GlobalService, messageService: MessageService, popups: PopupsService) {
    super(http, global, messageService, popups);
  }

  SignUp(command: SignUpUser): Observable<boolean> {
    return this.post<boolean>(`accounts`, command);
  }

  Login(command: UserLogin): Observable<string>{
    return this.post<string>(`accounts/login`, command);
  }

  CheckUserExistsAlready(username: string): Observable<boolean> {
    return this.get<boolean>(`accounts/check/${username}`);
  }

  Get(): Observable<User[]>{
    return this.get<User[]>(`accounts`);
  }

  GetById(id: number): Observable<User>{
    return this.get<User>(`accounts/${id}`);
  }

  GetByUsername(username: string): Observable<User>{
    return this.get<User>(`accounts/${username}`);
  }

  GetBy(query: UserQuery): Observable<User[]>{
    return this.post<User[]>(`accounts/by`, query);
  }

}
