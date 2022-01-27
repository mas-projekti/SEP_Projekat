import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayFormComponentComponent } from './pay-form-component.component';

describe('PayFormComponentComponent', () => {
  let component: PayFormComponentComponent;
  let fixture: ComponentFixture<PayFormComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PayFormComponentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PayFormComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
