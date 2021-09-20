import { Component, OnInit, Input, ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-details-log',
  templateUrl: './details-log.component.html',
  styleUrls: ['./details-log.component.css']
})
export class DetailsLogComponent implements OnInit {

  @ViewChild("div", {read: ElementRef}) div: ElementRef;
  @Input() details: HTMLDivElement

  constructor() { }

  ngOnInit(): void {
    setTimeout(() => {
      (this.div.nativeElement as HTMLElement).appendChild(this.details);
    }, 50);
  }

}
