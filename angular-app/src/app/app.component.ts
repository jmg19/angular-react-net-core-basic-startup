import { Component } from '@angular/core';
import { GlobalService } from './global.service';
import { PopupsService } from './popups.service';
import { TokenFactoryService } from './token-factory.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {

  constructor(
    private global: GlobalService,
    private tokenFactoryService: TokenFactoryService,
    public popups: PopupsService
    ){}

  title = 'app';

  ngOnInit() {
    this.tokenFactoryService.CheckToken()
  }
}
