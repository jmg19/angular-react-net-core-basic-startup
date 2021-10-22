import './SignUpPopup.css'
import React, { ChangeEvent, FormEvent, FocusEvent } from "react";
import { IFormStateData } from "../../helpers/forms/IFormStateData";
import { FormValidation } from '../../helpers/forms/FormValidation';
import { IAppServicesFactory, _IAppServicesFactory } from '../../AppServicesFactory';
import { IPopupsService } from '../../PopupsService';
import avatar from '../../assets/img_avatar4.png';
import { IAccountsService } from '../../api-services/AccountsService';
import { ILoginService } from '../../app-services/LoginService';

interface SignUpPopupStateData extends IFormStateData{
    username: string;
    password: string;
    confirmPassword: string;    
}

interface SignUpPopupPropsData {}

export class SignUpPopup extends React.Component<SignUpPopupPropsData, SignUpPopupStateData>{
    private servicesFactory: IAppServicesFactory = _IAppServicesFactory();
    private popupsService: IPopupsService = this.servicesFactory.IPopupsService; 
    private accountsService: IAccountsService = this.servicesFactory.IAccountsService;
    private loginService: ILoginService = this.servicesFactory.ILoginService;
    private formValidation: FormValidation;        

    constructor(props: SignUpPopupPropsData){
        super(props);

        this.state = {
            username: "",
            password: "",
            confirmPassword: '',
            errors: {
                username: {},
                password: {},
                confirmPassword: {}
            }
        }

        this.formValidation = new FormValidation(
            [
                {
                    fieldName: "username", 
                    validations: [
                        { validationKey: "required" },
                        { validationKey: "userExists" }
                    ]
                },
                {
                    fieldName: "password", 
                    validations: [
                        { validationKey: "required" },
                        { validationKey: "pattern", testValue: /(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.{8,})/, alternativeValidationKey: "passwordIsNotStrong" },
                        { validationKey: "pattern", testValue: /((?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.{6,}))|((?=.*[a-z])(?=.*[A-Z])(?=.*[^A-Za-z0-9])(?=.{8,}))|((?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,}))/, alternativeValidationKey: "passwordIsNotMedium" },
                    ]
                },
                {
                    fieldName: "confirmPassword", 
                    validations: [
                        { validationKey: "confirmPasswordMatch" }
                    ]
                }
            ]
        );

        this.formValidation.addCustomValidator("confirmPasswordMatch", (stateData: IFormStateData, fieldName: string) => {            
            const result = stateData["password"] !== stateData["confirmPassword"];
            stateData.errors[fieldName]["confirmPasswordMatch"] = result;
            return Promise.resolve(result);
        });

        this.formValidation.addCustomValidator("userExists", (stateData: IFormStateData, fieldName: string) => {            
            const thisAccountService = this.accountsService;
            return new Promise((resolve) => {
                if(!this.state.username){
                    resolve(false);
                    return;
                }
                
                thisAccountService.CheckUserExistsAlready(this.state.username).subscribe((doExist) => {
                    stateData.errors[fieldName]["userExists"] = doExist;
                    resolve(doExist);
                });
            });
        }, true);
    }

    componentDidMount(){}

    componentWillUnmount(){}

    Close = () => {
        this.popupsService.setShowPopupSignUp(false);
    }

    handleSubmit = (e: FormEvent) =>{
        e.preventDefault();
        const newState: SignUpPopupStateData = Object.assign({}, this.state);
        this.formValidation.validate(newState, true).then(() => this.setState(newState, () => {
            if(this.formValidation.isValid()){
                this.accountsService.SignUp({
                    UserName: this.state.username,
                    Password: this.state.password
                }).subscribe((r) => {
                    if(r){
                      this.loginService.Login(this.state.username, this.state.password, false);
                      this.Close();
                    }
                });;
            }
        }));
    }

    handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;

        this.setState({
            [name]: value
        }, () => {
            const newState: SignUpPopupStateData = Object.assign({}, this.state);
            if(name === "username") {
                this.formValidation.validate(newState, false, "userExists").then(() => this.setState(newState));   
            } else {
                this.formValidation.validate(newState).then(() => this.setState(newState));   
            }
        });
    }

    handleBlur = (e: FocusEvent<HTMLInputElement>) => {
        const newState: SignUpPopupStateData = Object.assign({}, this.state);        
        this.formValidation.validate(newState, true).then(() => this.setState(newState));  
    }

    isPasswordWeak:() => boolean = () => {
        return !!this.state.password && (this.state.errors?.password?.passwordIsNotStrong && this.state.errors?.password?.passwordIsNotMedium);
    }
    isPasswordMedium() {
        return !!this.state.password && (this.state.errors?.password?.passwordIsNotStrong && !this.state.errors?.password?.passwordIsNotMedium);
    }
    isPasswordStrong() {
        return !!this.state.password && (!this.state.errors?.password?.passwordIsNotStrong && !this.state.errors?.password?.passwordIsNotMedium);
    }

    render(){
        return (
            <div className="w3-modal" >
                <div className="w3-modal-content w3-card-4 w3-animate-zoom" style={{ maxWidth:"600px" }} >

                    <div className="w3-center"><br/>
                    <span onClick={this.Close} className="w3-button w3-xlarge w3-transparent w3-display-topright" title="Close Modal" >×</span>
                    <img src={avatar} alt="Avatar" style={{ width:"30%" }} className="w3-circle w3-margin-top" />
                    </div>

                    <form className="w3-container" onSubmit={this.handleSubmit}>
                    <div className="w3-section">
                        <label><b>Username</b></label>
                        <input className="w3-input w3-border" type="text" placeholder="Enter Username" name="username" value={this.state.username} onChange={this.handleChange} onBlur={ this.handleBlur }  />
                        <div className="form-validation-problems  w3-margin-bottom">
                            { this.state.errors?.username?.required && (<div>* Required</div>)}
                            { this.state.errors?.username?.userExists && (<div>* This user is already registed!</div>)}
                        </div>
                        <label><b>Password</b></label>
                        <input className="w3-input w3-border" type="password" placeholder="Enter Password" name="password" value={this.state.password} onChange={this.handleChange}  />
                        <div className="form-validation-problems  w3-margin-bottom">
                            { this.state.errors?.password?.required && (<div>* Required</div>)}
                            { this.isPasswordStrong() && (<div className="password-security-check strong">Strong</div>)}
                            { this.isPasswordMedium() && (<div className="password-security-check medium">Medium</div>)}
                            { this.isPasswordWeak() && (<div className="password-security-check weak">Weak</div>)}
                        </div>
                        <label><b>Confirm Password</b></label>
                        <input className="w3-input w3-border" type="password" placeholder="Enter Password" name="confirmPassword" value={this.state.confirmPassword} onChange={this.handleChange}  />
                        <div className="form-validation-problems  w3-margin-bottom">
                            { this.state.errors?.confirmPassword?.confirmPasswordMatch && (<div>* The password is not the same</div>)}
                        </div>
                        <button className="w3-button w3-block w3-green w3-section w3-padding" type="submit" >Sign Up</button>
                    </div>
                    </form>

                    <div className="w3-container w3-border-top w3-padding-16 w3-light-grey">
                    <button onClick={this.Close} type="button" className="w3-button w3-red">Cancel</button>
                    </div>

                </div>
            </div>
            );
    }    
}