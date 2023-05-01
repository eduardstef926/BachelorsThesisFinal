import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubscriptionPaymentPageComponent } from './subscription-payment-page.component';

describe('SubscriptionPaymentPageComponent', () => {
  let component: SubscriptionPaymentPageComponent;
  let fixture: ComponentFixture<SubscriptionPaymentPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubscriptionPaymentPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubscriptionPaymentPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
