import { AccountsService, IAccountsService } from "./api-services/AccountsService";
import { ISessionService, SessionService } from "./api-services/SessionService";
import { ILoginService, LoginService } from "./app-services/LoginService";
import { GlobalService, IGlobalService } from "./GlobalService";
import { IMessageService, MessageService } from "./MessageService";
import { IPopupsService, PopupsService } from "./PopupsService";
import { ITokenFactoryService, TokenFactoryService } from "./TokenFactoryService";

export interface IAppServicesFactory {
    _ILoginService(): ILoginService;
    _IGlobalService(): IGlobalService;
    _IPopupsService(): IPopupsService;
    _IMessageService(): IMessageService;
    _IAccountsService(): IAccountsService;
    _ISessionService(): ISessionService;
    _ITokenFactoryService(): ITokenFactoryService;    
}

class ServicesFactory implements IAppServicesFactory{
        
    _IGlobalService(): IGlobalService {
        if(!this.services["IGlobalService"]){            
            this.services["IGlobalService"] = new GlobalService(this._IPopupsService());
        }   

        return this.services["IGlobalService"];
    }

    _IPopupsService(): IPopupsService {
        if(!this.services["IPopupsService"]){            
            this.services["IPopupsService"] = new PopupsService();
        }   

        return this.services["IPopupsService"];
    }

    _IMessageService(): IMessageService {
        if(!this.services["IMessageService"]){            
            this.services["IMessageService"] = new MessageService();
        }   

        return this.services["IMessageService"];
    }

    _IAccountsService(): IAccountsService {
        if(!this.services["IAccountsService"]){                 
            this.services["IAccountsService"] = new AccountsService(this._IGlobalService(), this._IMessageService(), this._IPopupsService());
        }   

        return this.services["IAccountsService"];
    }

    _ISessionService(): ISessionService {
        if(!this.services["ISessionService"]) {            
            this.services["ISessionService"] = new SessionService(this._IGlobalService(), this._IMessageService(), this._IPopupsService());
        }   

        return this.services["ISessionService"];
    }

    _ITokenFactoryService(): ITokenFactoryService {
        if(!this.services["ITokenFactoryService"]) {            
            this.services["ITokenFactoryService"] = new TokenFactoryService(this._ISessionService(), this._IGlobalService());
        }   

        return this.services["ITokenFactoryService"];
    }

    _ILoginService(): ILoginService {
        if(!this.services["ILoginService"]) {            
            this.services["ILoginService"] = new LoginService(this._IGlobalService(), this._IAccountsService(), this._ISessionService(), this._ITokenFactoryService());
        }   

        return this.services["ILoginService"];
    }    
    private services: {[key: string]: any} = {};    
}

let servicesFactoryInstance: IAppServicesFactory;
export function _IAppServicesFactory(): IAppServicesFactory {
    if(!servicesFactoryInstance){
        servicesFactoryInstance = new ServicesFactory();
    }

    return servicesFactoryInstance;
}