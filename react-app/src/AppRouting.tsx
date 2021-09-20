import React from "react";
import { Home } from "./components/home/Home";
import { UsersList } from "./components/users-list/UsersList";
import { Switch, Route, Redirect } from "react-router-dom";
import { IAppServicesFactory, _IAppServicesFactory } from "./AppServicesFactory";
import { IGlobalService } from "./GlobalService";
import { Subscription } from "rxjs";

interface AppRoutingStateData{
    isUserLoggedIn: boolean;
}

interface AppRoutingPropsData{}

export class AppRouting extends React.Component<AppRoutingPropsData, AppRoutingStateData> {
    private servicesFactory: IAppServicesFactory = _IAppServicesFactory();
    private globalService: IGlobalService = this.servicesFactory._IGlobalService();
    private logedUserSubscription!: Subscription;
    
    private routes = [
        {
            path: "/users-list",
            component: <UsersList/>,
            requiresAuth: true
        },
        {
            path: "/",
            component: <Home/>
        }
    ];

    constructor(props: AppRoutingPropsData) {
        super(props);

        this.state = {            
            isUserLoggedIn: this.globalService.isUserLogedIn()
        }                      
    }

    componentDidMount(){
        this.logedUserSubscription = this.globalService.subscribeLogedUser(() => {            
            this.setState({isUserLoggedIn: this.globalService.isUserLogedIn()});
        });
    }
    
    componentWillUnmount(){
        this.logedUserSubscription.unsubscribe();
    }

    render(){
        return(
            <>
                <Switch>
                    {this.routes.map((route, i) => (
                        <Route key={i} path={route.path}>
                            { (!this.state.isUserLoggedIn && route.requiresAuth) ? <Redirect to="/" /> :  route.component}
                        </Route>
                    ))}
                </Switch>
            </>
        );
    }
}