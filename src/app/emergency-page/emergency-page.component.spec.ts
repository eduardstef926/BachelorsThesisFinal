import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmergencyPageComponent } from './emergency-page.component';

describe('EmergencyPageComponent', () => {
  let component: EmergencyPageComponent;
  let fixture: ComponentFixture<EmergencyPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmergencyPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmergencyPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
