import { BaseHttpClient } from './BaseHttpClient';
import { Observable } from 'rxjs';
import { User } from '../models/user';

export interface ISessionService {

  New() : Observable<string>;

  GetUser(token: string) : Observable<User>;
}

export class SessionService extends BaseHttpClient implements ISessionService {

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
