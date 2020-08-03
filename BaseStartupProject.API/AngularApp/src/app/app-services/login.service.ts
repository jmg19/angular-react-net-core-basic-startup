import { Injectable } from '@angular/core';
import { AccountsService } from '../api-services/accounts.service';
import { SessionService } from '../api-services/session.service';
import { GlobalService } from '../global.service';
import { UserLogin } from '../ServiceCommands/UserLogin';
import { TokenFactoryService } from '../token-factory.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private global: GlobalService, private accountService: AccountsService, private sessionService: SessionService, private tokenService: TokenFactoryService) { }

  public Login(username: string, password: string, toRemember: boolean):Promise<boolean>{
    let promisse = new Promise<boolean>((resolve, reject) => {
      const userLoginCommand: UserLogin = { UserName: username, Password: password };
      this.accountService.Login(userLoginCommand).subscribe((token) => {
        if(token){
          this.sessionService.GetUser(token).subscribe((user) => {
            if(user && user.id > 0){
              this.global.setToken(token, toRemember);
              this.global.setLogedUser(user);
              resolve(true);
            }
          });
        }else{
          resolve(false);
        }
      });
    });
    return promisse;
  }

  public LogOut(){
    this.global.setLogedUser({ id:0, username:"", active: false });
    localStorage.setItem("token", "");
    sessionStorage.setItem("token", "");
    this.tokenService.CheckToken();
  }
}
