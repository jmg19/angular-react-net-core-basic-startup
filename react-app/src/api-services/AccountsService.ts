import { User } from '../models/user';
import { BaseHttpClient } from './BaseHttpClient';
import { Observable } from 'rxjs';
import { SignUpUser } from '../ServiceCommands/SignUpUser'
import { UserLogin } from '../ServiceCommands/UserLogin';
import { UserQuery } from '../ServiceQueries/UserQuery';

export interface IAccountsService {
  SignUp(command: SignUpUser): Observable<boolean>;

  Login(command: UserLogin): Observable<string>;

  CheckUserExistsAlready(username: string): Observable<boolean>;

  Get(): Observable<User[]>;

  GetById(id: number): Observable<User>;

  GetByUsername(username: string): Observable<User>;

  GetBy(query: UserQuery): Observable<User[]>;
}

export class AccountsService extends BaseHttpClient implements IAccountsService {  
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
