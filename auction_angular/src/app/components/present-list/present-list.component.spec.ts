import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PresentListComponent } from './present-list.component';

describe('PresentListComponent', () => {
  let component: PresentListComponent;
  let fixture: ComponentFixture<PresentListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PresentListComponent]
    });
    fixture = TestBed.createComponent(PresentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
