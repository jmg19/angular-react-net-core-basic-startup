import { Injectable } from '@angular/core';

export enum LogMessageType{
  INFO,
  WARNING,
  ERROR
}

export class LogMessage{
  public type: LogMessageType;
  public message: string;
  public details: any;
  public date: Date;


  constructor(type: LogMessageType, message: string, details: any){
    this.type = type;
    this.message = message;
    this.details = details;
    this.date = new Date();
  }
}

@Injectable({
  providedIn: 'root',
})
export class MessageService {

  constructor(){
    (window as any).globalMessages = this.messages;
  }

  messages: LogMessage[] = [];

  add(message: string, type: LogMessageType, details: any) {
    this.messages.push(new LogMessage(type, message, details));
  }

  clear() {
    this.messages = [];
  }
}
