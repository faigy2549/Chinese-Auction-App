import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonarsAddComponent } from './donars-add.component';

describe('DonarsAddComponent', () => {
  let component: DonarsAddComponent;
  let fixture: ComponentFixture<DonarsAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DonarsAddComponent]
    });
    fixture = TestBed.createComponent(DonarsAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
