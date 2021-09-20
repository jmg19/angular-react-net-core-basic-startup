import { Injectable } from '@angular/core';
import { SessionService } from './api-services/session.service';
import { GlobalService } from './global.service';
import { User } from './models/user';


@Injectable({
  providedIn: 'root'
})
export class TokenFactoryService {

  constructor(
    private sessionService: SessionService,
    private global: GlobalService
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
