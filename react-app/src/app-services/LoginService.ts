import { IAccountsService } from "../api-services/AccountsService";
import { ISessionService } from "../api-services/SessionService";
import { IGlobalService } from "../GlobalService";
import { UserLogin } from "../ServiceCommands/UserLogin";
import { ITokenFactoryService } from "../TokenFactoryService";

export interface ILoginService{
    Login(username: string, password: string, toRemember: boolean): Promise<boolean>;
    LogOut(): void;
}


export class LoginService implements ILoginService{

    constructor(private global: IGlobalService, private accountService: IAccountsService, private sessionService: ISessionService, private tokenService: ITokenFactoryService) { }
  
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