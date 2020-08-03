import { Routes, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { HomeComponent } from "./components/home/home.component";
import { UsersListComponent } from "./components/users-list/users-list.component";

const routes: Routes = [
    // { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
    { path: '', component: HomeComponent },
    { path: 'list-users', component: UsersListComponent }
  ];
  
  @NgModule({
    imports: [ RouterModule.forRoot(routes) ],  
    exports: [ RouterModule ]
  })
  export class AppRoutingModule {}