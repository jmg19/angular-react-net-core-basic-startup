import { Injectable } from '@angular/core';
import { User } from './models/user';

@Injectable({
  providedIn: 'root'
})
export class GlobalService {
  private logedUser: User;

  constructor() { }

  public isUserLogedIn(): boolean{
    if(this.logedUser){
      return true;
    }

    return false;
  }
}
