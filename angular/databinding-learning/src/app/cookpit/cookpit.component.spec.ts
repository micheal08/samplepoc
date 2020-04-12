import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CookpitComponent } from './cookpit.component';

describe('CookpitComponent', () => {
  let component: CookpitComponent;
  let fixture: ComponentFixture<CookpitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CookpitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CookpitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
