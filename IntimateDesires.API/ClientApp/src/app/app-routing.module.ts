import { Routes, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { HomeComponent } from "./components/home/home.component";

const routes: Routes = [
    // { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
    { path: '', component: HomeComponent }
  ];
  
  @NgModule({
    imports: [ RouterModule.forRoot(routes) ],  
    exports: [ RouterModule ]
  })
  export class AppRoutingModule {}