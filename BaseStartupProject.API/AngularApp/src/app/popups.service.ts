import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PopupsService {

  private pendingProcesses: number = 1;
  public showPopupLogin: boolean;
  public showPopupSignUp: boolean;

  constructor() { }

  public showLoading(){
    return (this.pendingProcesses > 0)
  }

  public incrementPendingProcess(){
    this.pendingProcesses++;
  }

  public decrementPendingProcess(){
    this.pendingProcesses--;
  }
}
