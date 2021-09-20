import { Subject, Subscription } from "rxjs";

export interface IPopupsService{
  
  isPopupLogInActive(): boolean;
  setShowPopupLogin(value: boolean): void;
  
  isPopupSignUpActive(): boolean;
  setShowPopupSignUp(value: boolean): void;

  isLoadingActive(): boolean;

  incrementPendingProcess(): void;

  decrementPendingProcess(): void;

  subscribeShowLoading(callback: () => void): Subscription;

  subscribeIsPopupLoginActive(callback: () => void): Subscription

  subscribeIsPopupSignUpActive(callback: () => void): Subscription
}

export class PopupsService implements IPopupsService {
  private subjectShowLoading: Subject<void> = new Subject<void>();
  private subjectShowPopupLogin: Subject<void> = new Subject<void>();
  private subjectShowPopupSignUp: Subject<void> = new Subject<void>();
  
  private pendingProcesses: number = 1;      
  private showPopupLogin: boolean;
  private showPopupSignUp: boolean;
  
  constructor(){
    this.subjectShowLoading.next();
    this.showPopupLogin = false;
    this.showPopupSignUp = false;
  }

  isPopupLogInActive(): boolean {
    return this.showPopupLogin;
  }

  setShowPopupLogin(value: boolean): void {
    this.showPopupLogin = value;
    this.subjectShowPopupLogin.next();
  }

  isPopupSignUpActive(): boolean {
    return this.showPopupSignUp;
  }

  setShowPopupSignUp(value: boolean): void {
    this.showPopupSignUp = value;
    this.subjectShowPopupSignUp.next();
  }

  public isLoadingActive(){
    return (this.pendingProcesses > 0)
  }

  public incrementPendingProcess(){
    this.pendingProcesses++;
    this.subjectShowLoading.next();
  }

  public decrementPendingProcess(){
    this.pendingProcesses--;
    this.subjectShowLoading.next();
  }  

  subscribeShowLoading(callback: () => void): Subscription {
    return this.subjectShowLoading.subscribe({
      next: callback
    });
  }
  
  subscribeIsPopupLoginActive(callback: () => void): Subscription {
    return this.subjectShowPopupLogin.subscribe({
      next: callback
    });
  }

  subscribeIsPopupSignUpActive(callback: () => void): Subscription {
    return this.subjectShowPopupSignUp.subscribe({
      next: callback
    });
  }
}
