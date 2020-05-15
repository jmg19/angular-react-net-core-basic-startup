import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  @Input() show: boolean; 

  public username: string;
  public password: string;
  public toRemember: boolean;

  constructor() { }

  ngOnInit() {
  }

  public GoResist(){
    this.Close();

  }

  public Close(){
    this.show = false;    
  }
}
