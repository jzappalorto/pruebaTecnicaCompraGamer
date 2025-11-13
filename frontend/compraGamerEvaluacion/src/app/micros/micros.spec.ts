import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Micros } from './micros';

describe('Micros', () => {
  let component: Micros;
  let fixture: ComponentFixture<Micros>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Micros]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Micros);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
