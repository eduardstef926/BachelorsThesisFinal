import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppointmentSlotlistComponent } from './appointment-slotlist.component';

describe('AppointmentSlotlistComponent', () => {
  let component: AppointmentSlotlistComponent;
  let fixture: ComponentFixture<AppointmentSlotlistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AppointmentSlotlistComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppointmentSlotlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
