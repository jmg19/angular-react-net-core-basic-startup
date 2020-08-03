import { Injectable } from '@angular/core';
import { User } from './models/user';
import * as Fingerprint2 from 'fingerprintjs2';
import { PopupsService } from './popups.service';


@Injectable({
  providedIn: 'root'
})
export class GlobalService {
  private logedUser: User;
  private UUID: string = "";
  private token: string = "";

  constructor(private popups: PopupsService) {
    this.logedUser = new User();
  }

  public _UUID(): string{
    return this.UUID;
  }

  public _Token(): string{
    return this.token;
  }

  public setToken(token: string, toRemember: boolean){
    this.token = token;
    this.popups.decrementPendingProcess();
    if(toRemember){
      localStorage.setItem("token", token);
    } else {
      sessionStorage.setItem("token", token);
    }
  }

  public setLogedUser(user: User) {
    this.logedUser = user;
  }

  public setUUID() : Promise<void>{
    return new Promise<void>((resolve) => {
      if ((window as any).requestIdleCallback) {
        (window as any).requestIdleCallback(() => {
            Fingerprint2.getV18((fingerprint) => {
              this.UUID = fingerprint;
              resolve();
            });
        })
      } else {
          setTimeout(() => {
              Fingerprint2.getV18((fingerprint) => {
                this.UUID = fingerprint;
                resolve();
              });
          }, 500);
      }
    });
  }

  public isUserLogedIn(): boolean{
    if(this.logedUser && this.logedUser.id > 0){
      return true;
    }

    return false;
  }
}
