import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeesInARoleComponent } from './employees-in-arole.component';

describe('EmployeesInARoleComponent', () => {
  let component: EmployeesInARoleComponent;
  let fixture: ComponentFixture<EmployeesInARoleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeesInARoleComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmployeesInARoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
