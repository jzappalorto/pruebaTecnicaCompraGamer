import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Chicos } from './chicos.component';

describe('Chicos', () => {
  let component: Chicos;
  let fixture: ComponentFixture<Chicos>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Chicos]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Chicos);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
