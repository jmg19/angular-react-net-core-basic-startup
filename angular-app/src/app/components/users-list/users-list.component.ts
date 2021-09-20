import { Component, OnInit } from '@angular/core';
import { GlobalService } from 'src/app/global.service';
import { Router } from '@angular/router';
import { User } from '../../models/user';
import { AccountsService } from '../../api-services/accounts.service';
import { UserQuery } from 'src/app/ServiceQueries/UserQuery';
import { OrderingRule } from 'src/app/ServiceQueries/OrderingRule';
import { SearchRule, PagingRule } from 'src/app/ServiceQueries/SearchRule';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {

  public users: User[];
  private orderByIdDesc: boolean = false;
  private orderByUserNameDesc: boolean = false;
  private orderByActiveDesc: boolean = false;

  public byIdField: number;
  public byUsernameField: string = "";
  public byActiveField: boolean = false;
  public byInactiveField: boolean = false;

  constructor(private router: Router, private global: GlobalService, private accountsService: AccountsService) { }

  ngOnInit() {
    if(!this.global.isUserLogedIn()){
      this.router.navigate(["/"]);
    } else {
      this.accountsService.Get().subscribe(result => {
        if(result){
          this.users = result;
        }
      });
    }
  }

  OrderById(){
    this.orderByUserNameDesc = false;
    this.orderByActiveDesc = false;

    const ordering_rules: OrderingRule[] = [{ fieldName: "ID", isDescending: this.orderByIdDesc }];
    const query: UserQuery = { ordering_rules: ordering_rules, search_rules: [] };
    this.accountsService.GetBy(query).subscribe(result => {
      if(result){
        this.users = result;
        this.orderByIdDesc = !this.orderByIdDesc;
      }
    });
  }

  OrderByUserName(){
    this.orderByIdDesc = false;
    this.orderByActiveDesc = false;

    const ordering_rules: OrderingRule[] = [{ isDescending: this.orderByUserNameDesc, fieldName: "UserName" }];
    const query: UserQuery = { ordering_rules: ordering_rules, search_rules: [] };
    this.accountsService.GetBy(query).subscribe(result => {
      if(result){
        this.users = result;
        this.orderByUserNameDesc = !this.orderByUserNameDesc;
      }
    });
  }

  OrderByActive(){
    this.orderByIdDesc = false;
    this.orderByUserNameDesc = false;

    const ordering_rules: OrderingRule[] = [{ isDescending: this.orderByActiveDesc, fieldName: "Active" }];
    const query: UserQuery = { ordering_rules: ordering_rules, search_rules: [] };
    this.accountsService.GetBy(query).subscribe(result => {
      if(result){
        this.users = result;
        this.orderByActiveDesc = !this.orderByActiveDesc;
      }
    });
  }

  ActiveChange(){
    if(this.byActiveField){
      this.byInactiveField = false;
    }

    this.FilterList();
  }

  InactiveChange(){
    if(this.byInactiveField){
      this.byActiveField = false;
    }

    this.FilterList();
  }

  FilterList(){
    setTimeout(() => {
      const ordering_rules: OrderingRule[] = [];
      let search_rules: SearchRule[] = new Array<SearchRule>();

      if(this.byIdField > 0) {
        search_rules.push({ condition: `ID == ${this.byIdField}`, paging_rule: new PagingRule() });
      }

      if(this.byUsernameField){
        search_rules.push({ condition: `UserName.Contains("${this.byUsernameField}")`, paging_rule: new PagingRule() });
      }

      if(this.byInactiveField){
        search_rules.push({ condition: `!Active`, paging_rule: new PagingRule() });
      }

      if(this.byActiveField){
        search_rules.push({ condition: `Active`, paging_rule: new PagingRule() });
      }

      const query: UserQuery = { ordering_rules: ordering_rules, search_rules: search_rules };
      this.accountsService.GetBy(query).subscribe(result => {
        if(result){
          this.users = result;
        }
      });
    }, 50);
  }
}
