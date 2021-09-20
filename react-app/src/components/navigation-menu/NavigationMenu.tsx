import React from "react";
import { Link } from "react-router-dom";
import { Subscription } from "rxjs";
import { ILoginService } from "../../app-services/LoginService";
import { IAppServicesFactory, _IAppServicesFactory } from "../../AppServicesFactory";
import { IGlobalService } from "../../GlobalService";
import { IPopupsService } from "../../PopupsService";
import './NavigationMenu.css';


interface NavigationMenuStateData{
    isSideBarOpen: boolean;
    isUserLoggedIn: boolean;
}

interface NavigationMenuPropsData{}

export class NavigationMenu extends React.Component<NavigationMenuPropsData, NavigationMenuStateData>{
    private servicesFactory: IAppServicesFactory = _IAppServicesFactory();
    private popupsService: IPopupsService = this.servicesFactory._IPopupsService();
    private global: IGlobalService = this.servicesFactory._IGlobalService();
    private loginService: ILoginService = this.servicesFactory._ILoginService();
    private logedUserSubscription!: Subscription;

    constructor(props: NavigationMenuPropsData) {
        super(props);
    
        this.state = {
            isSideBarOpen: false,
            isUserLoggedIn: this.global.isUserLogedIn()
        }                      
      }
    
    openLoginPopup = () => {
        this.popupsService.setShowPopupLogin(true);        
    }

    openSignUpPopup = () => {
        this.popupsService.setShowPopupSignUp(true);
    }

    LogOut = () => {
        this.loginService.LogOut();
    }

    w3Toggle = () => {        
        if(this.state.isSideBarOpen){
            document.getElementById("main")!.style.marginLeft = "0%";
            document.getElementById("mySidebar")!.classList.remove("ShowSideBar");
        }else{
            document.getElementById("main")!.style.marginLeft = "125px";
            document.getElementById("mySidebar")!.classList.add("ShowSideBar");
        }
        
        this.setState((state) => ({ isSideBarOpen: !state.isSideBarOpen}) );
    }

    w3Close = () => {
        document.getElementById("main")!.style.marginLeft = "0%";
        document.getElementById("mySidebar")!.classList.remove("ShowSideBar");
        
        this.setState({isSideBarOpen: false});        
    }

    componentDidMount() {
        this.logedUserSubscription = this.global.subscribeLogedUser(() => {            
            this.setState({isUserLoggedIn: this.global.isUserLogedIn()});
        });
    }

    componentWillUnmount() {
        this.logedUserSubscription.unsubscribe();
    }

    render(){
        return (
        <>
            <div className="w3-top">
                <div id="menu-standard" className="w3-bar w3-black">
                    <button id="openNav" className="w3-button w3-teal w3-xlarge" onClick={this.w3Toggle}>&#9776;</button>
                    { !this.state.isUserLoggedIn && (<button className="w3-bar-item w3-button w3-right" onClick={ this.openSignUpPopup } >Sign Up</button>)}
                    { !this.state.isUserLoggedIn && (<button className="w3-bar-item w3-button w3-right" onClick={ this.openLoginPopup } >Login</button>)}
                    { this.state.isUserLoggedIn && (<button className="w3-bar-item w3-button" onClick={ this.LogOut } >Log Out</button>) }
                    <Link className="w3-bar-item w3-button w3-mobile w3-green w3-right" to="/">Home</Link>
                    { this.state.isUserLoggedIn && (<Link className="w3-bar-item w3-button w3-mobile w3-right" to="/users-list">List Users</Link>) }                        
                </div>
            </div>
            <div className="w3-sidebar w3-bar-block w3-card w3-animate-left" id="mySidebar">
                <button className="w3-bar-item w3-button w3-large" onClick={ this.w3Close }>Close &times;</button>
                { !this.state.isUserLoggedIn && (<button className="w3-bar-item w3-button" onClick={ this.openSignUpPopup } >Sign Up</button>) }
                { !this.state.isUserLoggedIn && (<button className="w3-bar-item w3-button" onClick={this.openLoginPopup} >Login</button>) }
                { this.state.isUserLoggedIn && (<button className="w3-bar-item w3-button" onClick={this.LogOut} >Log Out</button>) }        
                <Link className="w3-bar-item w3-button" to="/" >Home</Link>
                { this.state.isUserLoggedIn && (<Link className="w3-bar-item w3-button" to="/users-list" >List Users</Link>) }        
            </div>
        </>
        );
    }
}