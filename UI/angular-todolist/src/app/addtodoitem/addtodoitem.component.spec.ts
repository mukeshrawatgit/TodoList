import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddtodoitemComponent } from './addtodoitem.component';

describe('AddtodoitemComponent', () => {
  let component: AddtodoitemComponent;
  let fixture: ComponentFixture<AddtodoitemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddtodoitemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddtodoitemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
