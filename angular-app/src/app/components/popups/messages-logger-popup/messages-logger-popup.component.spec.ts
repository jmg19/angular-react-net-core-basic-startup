import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MessagesLoggerPopupComponent } from './messages-logger-popup.component';

describe('MessagesLoggerPopupComponent', () => {
  let component: MessagesLoggerPopupComponent;
  let fixture: ComponentFixture<MessagesLoggerPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MessagesLoggerPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MessagesLoggerPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
