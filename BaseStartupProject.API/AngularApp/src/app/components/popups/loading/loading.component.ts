import { Component, OnInit } from '@angular/core';
import { PopupsService } from 'src/app/popups.service';
import { MessageService, LogMessageType, LogMessage } from 'src/app/message.service';

@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.css']
})
export class LoadingComponent implements OnInit {

  errors: LogMessage[] = new Array();
  showErros: boolean = false;

  constructor(public popups: PopupsService, private messageService: MessageService) { }

  ngOnInit(): void {
  }

  thereAreErrors(): boolean{
    this.errors = this.messageService.messages.filter((value) => value.type = LogMessageType.ERROR);
    return (this.errors.length > 0);
  }

  openMessagesPopup(){
    this.showErros = true;
  }
}
