import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { LogMessage } from 'src/app/message.service';
import JSONFormatter from 'json-formatter-js'

@Component({
  selector: 'app-messages-logger-popup',
  templateUrl: './messages-logger-popup.component.html',
  styleUrls: ['./messages-logger-popup.component.css']
})
export class MessagesLoggerPopupComponent implements OnInit {

  @Input() messages: LogMessage[];
  @Input() show: boolean;
  @Output() showChange = new EventEmitter<boolean>();
  formatedLog: HTMLDivElement;

  constructor() { }

  ngOnInit(): void {

  }

  private log () {

  }

  public renderDetais(details: any): HTMLDivElement{
    if((typeof(details) !== "object")){
      let html = new HTMLDivElement();
      html.innerHTML = details;
      return html;
    } else {
      return (new JSONFormatter(details)).render();
    }
  }

  hide(){
    this.show = false;
    this.showChange.emit(this.show);
  }
}
