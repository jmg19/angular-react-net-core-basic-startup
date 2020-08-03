//Modules
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';

//api-services
import { AccountsService } from './api-services/accounts.service';
import { SessionService } from './api-services/session.service';

//app-services
import { LoginService } from './app-services/login.service';

//base-services
import { GlobalService } from './global.service';
import { MessageService } from './message.service';
import { PopupsService } from './popups.service';
import { TokenFactoryService } from "./token-factory.service"

//Components
import { AppComponent } from './app.component';
import { DetailsLogComponent } from './components/popups/messages-logger-popup/details-log/details-log.component';
import { HomeComponent } from './components/home/home.component';
import { LoadingComponent } from './components/popups/loading/loading.component';
import { LoginPopupComponent } from './components/popups/login-popup/login-popup.component';
import { MessagesLoggerPopupComponent } from './components/popups/messages-logger-popup/messages-logger-popup.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { SignUpPopupComponent } from './components/popups/sign-up-popup/sign-up-popup.component';
import { UsersListComponent } from './components/users-list/users-list.component';

//Directives
import { CheckUserExistDirective } from './directives/check-user-exist.directive';

@NgModule({
  declarations: [
    AppComponent,
    CheckUserExistDirective,
    DetailsLogComponent,
    HomeComponent,
    LoadingComponent,
    LoginPopupComponent,
    MessagesLoggerPopupComponent,
    NavMenuComponent,
    SignUpPopupComponent,
    UsersListComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    AccountsService,
    GlobalService,
    LoginService,
    MessageService,
    PopupsService,
    SessionService,
    TokenFactoryService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
