import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SymptomPageComponent } from './symptom-page.component';

describe('SymptomPageComponent', () => {
  let component: SymptomPageComponent;
  let fixture: ComponentFixture<SymptomPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SymptomPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SymptomPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
