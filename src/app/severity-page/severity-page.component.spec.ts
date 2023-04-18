import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeverityPageComponent } from './severity-page.component';

describe('SecondPhaseComponent', () => {
  let component: SeverityPageComponent;
  let fixture: ComponentFixture<SeverityPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SeverityPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SeverityPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
