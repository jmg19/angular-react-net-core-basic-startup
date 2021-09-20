import { Component, OnInit } from '@angular/core';
import { PopupsService } from './../../../popups.service';
import { AccountsService } from 'src/app/api-services/accounts.service';
import { SignUpUser } from 'src/app/ServiceCommands/SignUpUser';
import { UserLogin } from 'src/app/ServiceCommands/UserLogin';
import { GlobalService } from 'src/app/global.service';
import { LoginService } from 'src/app/app-services/login.service';

@Component({
  selector: 'app-sign-up-popup',
  templateUrl: './sign-up-popup.component.html',
  styleUrls: ['./sign-up-popup.component.css']
})
export class SignUpPopupComponent implements OnInit {

  public username: string;
  public password: string;

  constructor(private global: GlobalService, private popups: PopupsService, private accountsService: AccountsService, private loginService: LoginService) { }

  ngOnInit() {
  }

  public Close(){
    this.popups.showPopupSignUp = false;
  }

  public SignUpNewUser(){
    const signUpUserCommand: SignUpUser = { UserName: this.username, Password: this.password };
    this.accountsService.SignUp(signUpUserCommand).subscribe((r) => {
      if(r){
        this.loginService.Login(this.username, this.password, false);
        this.Close();
      }
    });
  }
}
