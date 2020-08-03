import { Component } from '@angular/core';
import { GlobalService } from 'src/app/global.service';
import { PopupsService } from 'src/app/popups.service';
import { LoginService } from 'src/app/app-services/login.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isSideBarOpen = false;

  constructor(
    private global: GlobalService,
    private popups: PopupsService,
    private loginService: LoginService
    ){}

  openLoginPopup() {
    this.popups.showPopupLogin = true;
  }

  openSignUpPopup() {
    this.popups.showPopupSignUp = true;
  }

  isUserLoggedIn(){
    return this.global.isUserLogedIn();
  }

  LogOut(){
    this.loginService.LogOut();
  }

  w3_toggle() {
    if(this.isSideBarOpen){
      document.getElementById("main").style.marginLeft = "0%";
      document.getElementById("mySidebar").classList.remove("ShowSideBar");
    }else{
      document.getElementById("main").style.marginLeft = "125px";
      document.getElementById("mySidebar").classList.add("ShowSideBar");
    }

    this.isSideBarOpen = !this.isSideBarOpen;
  }

  w3_close() {
    document.getElementById("main").style.marginLeft = "0%";
    document.getElementById("mySidebar").classList.remove("ShowSideBar");

    this.isSideBarOpen = false;
  }
}
