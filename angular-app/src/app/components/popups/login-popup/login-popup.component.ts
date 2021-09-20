import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { PopupsService } from './../../../popups.service';
import { LoginService } from 'src/app/app-services/login.service';

@Component({
  selector: 'app-login-popup',
  templateUrl: './login-popup.component.html',
  styleUrls: ['./login-popup.component.css']
})
export class LoginPopupComponent implements OnInit {
  public username: string;
  public password: string;
  public toRemember: boolean;
  public wrong_credentials: boolean = false;

  constructor(private popups: PopupsService, private loginService: LoginService) { }

  ngOnInit() {

  }

  public DoLogin(valid: boolean){
    this.wrong_credentials = false;
    if(valid){
      this.loginService.Login(this.username, this.password, this.toRemember).then(result => {
        if(result){
          this.Close();
        } else {
          this.wrong_credentials = true;
        }
      });
    }
  }

  public GoResist(){
    this.Close();
    this.popups.showPopupSignUp = true;
  }

  public Close(){
    this.popups.showPopupLogin = false;
  }
}
