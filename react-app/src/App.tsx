import React from 'react';
import logo from './logo.svg';
import './App.css';
import { Loading } from './components/loading/Loading';
import { _IAppServicesFactory, IAppServicesFactory } from './AppServicesFactory';
import { IPopupsService } from './PopupsService';
import { Subscription } from 'rxjs';
import { AppRouting } from './AppRouting';
import { NavigationMenu } from './components/navigation-menu/NavigationMenu';
import { LoginPopup } from './components/login-popup/LoginPopup';
import { SignUpPopup } from './components/sign-up-popup/SignUpPopup';
import { IGlobalService } from './GlobalService';

interface AppStateData{
  isLoading: boolean,
  isPopupLogInActive: boolean,
  isPopupSignUpActive: boolean,
  isCheckingToken: boolean
}

interface AppPropsData{

}

export class App extends React.Component<AppPropsData, AppStateData> {
  private servicesFactory: IAppServicesFactory = _IAppServicesFactory();
  private popupsService: IPopupsService = this.servicesFactory._IPopupsService();
  private globalService: IGlobalService = this.servicesFactory._IGlobalService();
  private showLoadingSubscription!: Subscription;
  private isPopupLogInActiveSubscription!: Subscription;
  private isPopupSignUpActiveSubscription!: Subscription;
  private isCheckingTokenSubscription!: Subscription;
  
  constructor(props: AppPropsData) {
    super(props);

    this.state = {
        isLoading: this.popupsService.isLoadingActive(),
        isPopupLogInActive: this.popupsService.isPopupLogInActive(),
        isPopupSignUpActive: this.popupsService.isPopupSignUpActive(),
        isCheckingToken: !this.globalService._Token()
    }                      
  }

  componentDidMount(){
    const tokenFactoryService = this.servicesFactory._ITokenFactoryService();
    tokenFactoryService.CheckToken();   

    this.showLoadingSubscription = this.popupsService.subscribeShowLoading(() => {      
      this.setState({isLoading: this.popupsService.isLoadingActive()});
    });

    this.isPopupLogInActiveSubscription = this.popupsService.subscribeIsPopupLoginActive(() => {      
      this.setState({isPopupLogInActive: this.popupsService.isPopupLogInActive()});
    });

    this.isPopupSignUpActiveSubscription = this.popupsService.subscribeIsPopupSignUpActive(() => {      
      this.setState({isPopupSignUpActive: this.popupsService.isPopupSignUpActive()});
    });

    if(this.state.isCheckingToken){
      this.isCheckingTokenSubscription = this.globalService.subscribeIsCheckingToken(() => {      
        const isCheckingToken: boolean = !this.globalService._Token();
        if(!isCheckingToken){
          this.setState({isCheckingToken: false});
          this.isCheckingTokenSubscription.unsubscribe();
        }
      });
    }
  }

  componentWillUnmount(){
      this.showLoadingSubscription.unsubscribe();
      this.isPopupLogInActiveSubscription.unsubscribe();
      this.isPopupSignUpActiveSubscription.unsubscribe();
  }
  
  render(){
    return (
      <div className="App" id="main">
        <img src={logo} className="App-logo" alt="logo" />
        <NavigationMenu />
        <div className="w3-container w3-margin-top">
          { !this.state.isCheckingToken && <AppRouting/> }
        </div>
        { this.state.isPopupLogInActive && (<LoginPopup />) }
        { this.state.isPopupSignUpActive && (<SignUpPopup />) }
        { this.state.isLoading && (<Loading />) }
      </div>
    );
  }
  
}


