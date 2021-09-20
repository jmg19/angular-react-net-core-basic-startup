import { User } from './models/user';
import FingerprintJS from '@fingerprintjs/fingerprintjs'
import { IPopupsService } from './PopupsService';
import { Subject, Subscription } from 'rxjs';

export interface IGlobalService {
  _UUID() : string;
  _Token(): string;
  setToken(token: string, toRemember: boolean): void;
  setLogedUser(user: User): void; 
  setUUID() : Promise<void>;
  isUserLogedIn(): boolean;
  subscribeLogedUser(callback: () => void): Subscription;
  subscribeIsCheckingToken(arg0: () => void): Subscription;
}

export class GlobalService implements IGlobalService {
  private subjectLogedUser: Subject<void> = new Subject<void>();
  private subjectToken: Subject<void> = new Subject<void>();

  private logedUser: User;
  private UUID: string = "";
  private token: string = "";

  constructor(private loadingService: IPopupsService) {
    this.logedUser = { id:0, username: "", active:false };
  }

  public _UUID(): string{
    return this.UUID;
  }

  public _Token(): string{
    return this.token;
  }

  public setToken(token: string, toRemember: boolean){
    this.token = token;
    this.subjectToken.next();
    this.loadingService.decrementPendingProcess();
    if(toRemember){
      localStorage.setItem("token", token);
    } else {
      sessionStorage.setItem("token", token);
    }
  }

  public setLogedUser(user: User) {
    this.logedUser = user;
    this.subjectLogedUser.next();
  }

  public setUUID() : Promise<void>{
    return new Promise<void>((resolve) => {
      if ((window as any).requestIdleCallback) {
        (window as any).requestIdleCallback(() => {
          (async () => {
            const agent = await FingerprintJS.load();
            const result = await agent.get();
            this.UUID = result.visitorId;
            resolve();
          })();            
        })
      } else {
        (async () => {
          const agent = await FingerprintJS.load();
          const result = await agent.get();
          this.UUID = result.visitorId;
          resolve();
        })();
      }
    });
  }

  public isUserLogedIn(): boolean{
    if(this.logedUser && this.logedUser.id > 0){
      return true;
    }

    return false;
  }

  subscribeLogedUser(callback: () => void): Subscription {
    return this.subjectLogedUser.subscribe({
      next: callback
    });
  }

  
  subscribeIsCheckingToken(callback: () => void): Subscription {
    return this.subjectToken.subscribe({
      next: callback
    });
  }
}
