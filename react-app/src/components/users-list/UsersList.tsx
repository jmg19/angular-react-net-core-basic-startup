import React, { ChangeEvent } from "react";
import { IAccountsService } from "../../api-services/AccountsService";
import { IAppServicesFactory, _IAppServicesFactory } from "../../AppServicesFactory";
import { User } from "../../models/user";
import { OrderingRule } from "../../ServiceQueries/OrderingRule";
import { PagingRule, SearchRule } from "../../ServiceQueries/SearchRule";
import { UserQuery } from "../../ServiceQueries/UserQuery";

interface UsersListStateData {
  users: User[];
  orderByIdDesc: boolean;
  orderByUserNameDesc: boolean;
  orderByActiveDesc: boolean;
  byIdField: number | "";
  byUsernameField: string;
  byActiveField: boolean;
  byInactiveField: boolean;
  [key: string]: any;
}

interface UsersListPropsData {}

export class UsersList extends React.Component<UsersListPropsData, UsersListStateData>{
    private servicesFactory: IAppServicesFactory = _IAppServicesFactory();     
    private accountsService: IAccountsService = this.servicesFactory.IAccountsService;
    
    constructor(props: UsersListPropsData) {
        super(props);

        this.state = {
            users: [],
            byActiveField: false,
            byIdField: "",
            byInactiveField: false,
            byUsernameField: "",
            orderByActiveDesc: false,
            orderByIdDesc: false,
            orderByUserNameDesc: false
        };
    }

    componentDidMount(){
        this.accountsService.Get().subscribe(result => {
            if(result){
                this.setState({ users: result });
            }
        });
    }

    OrderById(){
        this.setState(
            {
                orderByUserNameDesc: false,
                orderByActiveDesc: false
            }, 
            () => {            
                const ordering_rules: OrderingRule[] = [{ fieldName: "ID", isDescending: this.state.orderByIdDesc }];
                const query: UserQuery = { ordering_rules: ordering_rules, search_rules: [] };
                this.accountsService.GetBy(query).subscribe(result => {
                    if(result){
                        this.setState((state) => ({
                            users: result,
                            orderByIdDesc: !state.orderByIdDesc
                        }))                        
                    }
                });
        });
    }

    OrderByUserName(){
        this.setState(
            {
                orderByIdDesc: false,
                orderByActiveDesc: false
            }, 
            () => {            
                const ordering_rules: OrderingRule[] = [{ isDescending: this.state.orderByUserNameDesc, fieldName: "UserName" }];
                const query: UserQuery = { ordering_rules: ordering_rules, search_rules: [] };
                this.accountsService.GetBy(query).subscribe(result => {
                    if(result){
                        this.setState((state) => ({
                            users: result,
                            orderByUserNameDesc: !state.orderByUserNameDesc
                        }))                        
                    }
                });
        });
    }

    OrderByActive(){
        this.setState(
            {
                orderByIdDesc: false,
                orderByUserNameDesc: false
            }, 
            () => {            
                const ordering_rules: OrderingRule[] = [{ isDescending: this.state.orderByActiveDesc, fieldName: "Active" }];
                const query: UserQuery = { ordering_rules: ordering_rules, search_rules: [] };
                this.accountsService.GetBy(query).subscribe(result => {
                    if(result){
                        this.setState((state) => ({
                            users: result,
                            orderByActiveDesc: !state.orderByActiveDesc
                        }))                        
                    }
                });
        });
    }

    ActiveChange(){
        if(this.state.byActiveField){            
            this.setState({
                byInactiveField: false
            });
        }

        this.FilterList();
    }

    InactiveChange(){
        if(this.state.byInactiveField){
            this.setState({
                byActiveField: false
            });            
        }

        this.FilterList();
    }
    
    FilterList(){
        setTimeout(() => {
            const ordering_rules: OrderingRule[] = [];
            let search_rules: SearchRule[] = [];
            const emptyPagingRule: PagingRule = { page:0, pageSize:0 };

            if(this.state.byIdField > 0) {
                search_rules.push({ condition: `ID == ${this.state.byIdField}`, pagingRule: emptyPagingRule });
            }

            if(this.state.byUsernameField){
                search_rules.push({ condition: `UserName.Contains("${this.state.byUsernameField}")`, pagingRule: emptyPagingRule });
            }

            if(this.state.byInactiveField){
                search_rules.push({ condition: `!Active`, pagingRule: emptyPagingRule });
            }

            if(this.state.byActiveField){
                search_rules.push({ condition: `Active`, pagingRule: emptyPagingRule });
            }

            const query: UserQuery = { ordering_rules: ordering_rules, search_rules: search_rules };
            this.accountsService.GetBy(query).subscribe(result => {
                if(result){
                    this.setState({
                        users: result
                    });                    
                }
            });
        }, 50);
    }

    handleChange = (e: ChangeEvent<HTMLInputElement>, callback: () => void) => {
        const { name, value } = e.target;        
        this.setState({ [name]: value }, callback);
    }

    handleCheckboxChange = (e: ChangeEvent<HTMLInputElement>, callback: () => void) => {
        const { name, checked } = e.target;
        this.setState({[name]: checked }, callback);
    }
    
    render(){
        return(
            <div className="w3-container w3-margin-top">
                <h2>Users List</h2>
                
                <table className="w3-table-all w3-card-4">
                    <tr>
                    <th>
                        <span className="w3-cursor-pointer" onClick={this.OrderById} >ID</span>
                        <input style={{marginLeft: "5px"}} type="number" name="byIdField" onChange={ (e: ChangeEvent<HTMLInputElement>) => this.handleChange(e, this.FilterList) } value={ this.state.byIdField } />
                    </th>
                    <th>
                        <span className="w3-cursor-pointer" onClick={ this.OrderByUserName } >Username</span>
                        <input style={{marginLeft: "5px"}} type="text" name="byUsernameField" onChange={ (e: ChangeEvent<HTMLInputElement>) => this.handleChange(e, this.FilterList) } value={ this.state.byUsernameField } />
                    </th>
                    <th>
                        <input type="checkbox" id="user-active" name="byActiveField" onChange={ (e: ChangeEvent<HTMLInputElement>) => this.handleCheckboxChange(e, this.ActiveChange) } checked={ this.state.byActiveField } />
                        &nbsp;
                        <label htmlFor="user-active" >active</label>
                        &nbsp;
                        <input type="checkbox" id="user-inactive" name="byInactiveField" onChange={ (e: ChangeEvent<HTMLInputElement>) => this.handleCheckboxChange(e, this.InactiveChange) } checked={ this.state.byInactiveField } />
                        &nbsp;
                        <label htmlFor="user-inactive" >inactive</label>
                    </th>
                    </tr>
                    { this.state.users.map((user, key) => 
                        (
                        <tr key={key}>
                            <td>{user.id}</td>
                            <td>{user.username}</td>
                            <td>{user.active ? "Active" : "Inactive" }</td>
                        </tr>
                        )
                    )}
                </table>
            </div>
        );
    }
}