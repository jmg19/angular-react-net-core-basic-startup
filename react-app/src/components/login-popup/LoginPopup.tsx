import './LoginPopup.css'
import React, { ChangeEvent, FormEvent } from "react";
import { IAppServicesFactory, _IAppServicesFactory } from '../../AppServicesFactory';
import { IPopupsService } from '../../PopupsService';
import { ILoginService } from '../../app-services/LoginService';
import { IFormStateData } from '../../helpers/forms/IFormStateData';
import { FormValidation } from '../../helpers/forms/FormValidation';
import avatar from '../../assets/img_avatar4.png';

interface LoginPopupStateData extends IFormStateData{
    username: string;
    password: string;
    toRemember: boolean;
    wrongCredentials: boolean;    
}

interface LoginPopupPropsData {}

export class LoginPopup extends React.Component<LoginPopupPropsData, LoginPopupStateData>{
    private servicesFactory: IAppServicesFactory = _IAppServicesFactory();
    private popupsService: IPopupsService = this.servicesFactory.IPopupsService; 
    private loginService: ILoginService = this.servicesFactory.ILoginService;
    private formValidation: FormValidation;    
        
    constructor(props: LoginPopupPropsData){
        super(props);

        this.state = {
            username: "",
            password: "",
            toRemember: false,
            wrongCredentials: false,
            errors: {
                username: {},
                password: {}
            }
        };

        this.formValidation = new FormValidation(
            [
                {
                    fieldName: "username", 
                    validations: [
                        { validationKey: "required" }
                    ]
                },
                {
                    fieldName: "password", 
                    validations: [
                        { validationKey: "required" }
                    ]
                }
            ]
        );
    }

    public DoLogin(valid: boolean){
        this.setWrongCredentials(false);
        if(valid){
            this.loginService.Login(this.state.username, this.state.password, this.state.toRemember).then(result => {
                if(result){
                    this.Close();
                } else {
                    this.setWrongCredentials(true);
                }
            });
        }
    }

    setUsername = (value: string) => {        
        this.setState({username: value});
    }
    
    setPassword = (value: string) => {        
        this.setState({password: value});
    }
    
    setToRemember = (value: boolean) => {        
        this.setState({toRemember: value});
    }

    setWrongCredentials = (value: boolean) => {        
        this.setState({wrongCredentials: value});
    }
    
    GoResist = () => {
        this.Close();
        this.popupsService.setShowPopupSignUp(true); 
    }

    Close = () => {
        this.popupsService.setShowPopupLogin(false);
    }

    handleSubmit = (e: FormEvent) =>{
        e.preventDefault();
        const newState: LoginPopupStateData = Object.assign({}, this.state);
        this.formValidation.validate(newState).then(() => this.setState(newState));
        if(this.formValidation.isValid()){
            this.loginService.Login(this.state.username, this.state.password, this.state.toRemember).then(result => {
                if(result){
                    this.setState({wrongCredentials: true});
                    this.Close();
                } else {
                    this.setState({wrongCredentials: true});
                }
            });
        }
    }

    handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        this.setState({[name]: value }, () => {
            const newState: LoginPopupStateData = Object.assign({}, this.state);
            this.formValidation.validate(newState).then(() => this.setState(newState));   
        });
    }

    handleCheckboxChange = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, checked } = e.target;
        this.setState({[name]: checked });
    }

    componentDidMount(){

    }

    componentWillUnmount(){
        
    }

    render(){
        return(
            <div className="w3-modal" >
            <div className="w3-modal-content w3-card-4 w3-animate-zoom" style={{ maxWidth: "600px" }}>          
              <div className="w3-center"><br/>
                <span onClick={ this.Close } className="w3-button w3-xlarge w3-transparent w3-display-topright" title="Close Modal" >×</span>
                <img src={avatar} alt="Avatar" style={{ width: "30%" }} className="w3-circle w3-margin-top" />
              </div>
          
              <form id="formLogin" className="w3-container" onSubmit={ this.handleSubmit } >
                <div className="w3-section">
                  <label><b>Username</b></label>
                  <input className="w3-input w3-border" type="text" placeholder="Enter Username" name="username" value={this.state.username} onChange={this.handleChange} />
                  <div className="form-validation-problems  w3-margin-bottom">
                    { this.state.errors?.username?.required && (<div>* Required</div>)}
                  </div>
                  <label><b>Password</b></label>
                  <input className="w3-input w3-border" type="password" placeholder="Enter Password" name="password" value={this.state.password} onChange={this.handleChange} />
                  <div className="form-validation-problems  w3-margin-bottom">
                  { this.state.errors?.password?.required && (<div>* Required</div>)}
                  </div>
                  <button className="w3-button w3-block w3-green w3-section w3-padding" type="submit" >Login</button>
                  <input className="w3-check w3-margin-top" type="checkbox" name="toRemember" checked={this.state.toRemember} onChange={this.handleCheckboxChange} /><span>Remember me</span>
                  <div className="form-validation-problems  w3-margin-top">
                    {this.state.wrongCredentials && (<div>* Wrong Credentials</div>)}
                  </div>
                </div>
              </form>
          
              <div className="w3-container w3-border-top w3-padding-16 w3-light-grey">
                <button onClick={this.Close} type="button" className="w3-button w3-red">Cancel</button>
                <span className="w3-right w3-padding w3-hide-small"><span onClick={this.GoResist} className="w3-hyperlink">New Accout?</span></span>
              </div>
          
            </div>
          </div>
            );
    }
}