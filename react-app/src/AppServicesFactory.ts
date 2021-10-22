import { AccountsService, IAccountsService } from "./api-services/AccountsService";
import { ISessionService, SessionService } from "./api-services/SessionService";
import { ILoginService, LoginService } from "./app-services/LoginService";
import { GlobalService, IGlobalService } from "./GlobalService";
import { IMessageService, MessageService } from "./MessageService";
import { IPopupsService, PopupsService } from "./PopupsService";
import { ITokenFactoryService, TokenFactoryService } from "./TokenFactoryService";

export interface IAppServicesFactory {
    ILoginService: ILoginService;
    IGlobalService: IGlobalService;
    IPopupsService: IPopupsService;
    IMessageService: IMessageService;
    IAccountsService: IAccountsService;
    ISessionService: ISessionService;
    ITokenFactoryService: ITokenFactoryService;    
}

class ServicesFactory implements IAppServicesFactory{
        
    get IGlobalService(): IGlobalService {
        if(!this.services["IGlobalService"]){            
            this.services["IGlobalService"] = new GlobalService(this.IPopupsService);
        }   

        return this.services["IGlobalService"];
    }

    get IPopupsService(): IPopupsService {
        if(!this.services["IPopupsService"]){            
            this.services["IPopupsService"] = new PopupsService();
        }   

        return this.services["IPopupsService"];
    }

    get IMessageService(): IMessageService {
        if(!this.services["IMessageService"]){            
            this.services["IMessageService"] = new MessageService();
        }   

        return this.services["IMessageService"];
    }

    get IAccountsService(): IAccountsService {
        if(!this.services["IAccountsService"]){                 
            this.services["IAccountsService"] = new AccountsService(this.IGlobalService, this.IMessageService, this.IPopupsService);
        }   

        return this.services["IAccountsService"];
    }

    get ISessionService(): ISessionService {
        if(!this.services["ISessionService"]) {            
            this.services["ISessionService"] = new SessionService(this.IGlobalService, this.IMessageService, this.IPopupsService);
        }   

        return this.services["ISessionService"];
    }

    get ITokenFactoryService(): ITokenFactoryService {
        if(!this.services["ITokenFactoryService"]) {            
            this.services["ITokenFactoryService"] = new TokenFactoryService(this.ISessionService, this.IGlobalService);
        }   

        return this.services["ITokenFactoryService"];
    }

    get ILoginService(): ILoginService {
        if(!this.services["ILoginService"]) {            
            this.services["ILoginService"] = new LoginService(this.IGlobalService, this.IAccountsService, this.ISessionService, this.ITokenFactoryService);
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