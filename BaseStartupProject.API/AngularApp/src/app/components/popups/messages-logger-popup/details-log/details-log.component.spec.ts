import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailsLogComponent } from './details-log.component';

describe('DetailsLogComponent', () => {
  let component: DetailsLogComponent;
  let fixture: ComponentFixture<DetailsLogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetailsLogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailsLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
