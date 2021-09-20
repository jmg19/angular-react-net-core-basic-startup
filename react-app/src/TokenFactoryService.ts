import { ISessionService } from "./api-services/SessionService";
import { IGlobalService } from "./GlobalService";
import { User } from "./models/user";


export interface ITokenFactoryService{
  CheckToken(): void;
}


export class TokenFactoryService implements ITokenFactoryService{
  constructor(
    private sessionService: ISessionService,
    private global: IGlobalService
    ) { }

    CheckToken(){
      this.global.setUUID().then(() => {
        const tokenValueInLocalStorage = localStorage.getItem("token");
        const tokenValueInSessionStorage = sessionStorage.getItem("token");

        if(tokenValueInLocalStorage) {
          this.GetUserFromToken(tokenValueInLocalStorage).then((user) => {
            if(user){
              this.global.setLogedUser(user);
              this.global.setToken(tokenValueInLocalStorage, true);
            } else {
              this.GenerateToken();
            }
          });
        } else if(tokenValueInSessionStorage) {
          this.GetUserFromToken(tokenValueInSessionStorage).then((user) => {
            if(user){
              this.global.setLogedUser(user);
              this.global.setToken(tokenValueInSessionStorage, false);
            } else {
              this.GenerateToken();
            }
          });
        } else {
          this.GenerateToken();
        }
      });
    }

    private GenerateToken() {
      this.sessionService.New().subscribe((token) => {
        if(token)
        {
          this.global.setToken(token, false);
        }
      });
    }

    private GetUserFromToken(token: string) : Promise<User>{
      return new Promise<User>((resolve) => {
        this.sessionService.GetUser(token).subscribe((user) => {
          resolve(user);
        });
      });
    }
}
