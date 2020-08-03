import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseHttpClient } from './base-http-client';
import { GlobalService } from '../global.service';
import { MessageService } from '../message.service';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { PopupsService } from '../popups.service';

@Injectable({
  providedIn: 'root'
})
export class SessionService extends BaseHttpClient {

  constructor(http: HttpClient, global: GlobalService, messageService: MessageService, popups: PopupsService) {
    super(http, global, messageService, popups);
  }

  New() : Observable<string>{
    return this.get<string>("session/new");
  }

  GetUser(token: string) : Observable<User>{
    var data = {
      token: token
    };

    return this.post<User>(`session/validate-token`, data);
  }
}
