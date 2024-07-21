import { Component} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Employee, EmployeeAPIService } from '../employee-api.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-employees-in-arole',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './employees-in-arole.component.html',
  styleUrl: './employees-in-arole.component.css'
})
export class EmployeesInARoleComponent {

  employees:Employee[]=[];
  rolesId: number|null =null;

  constructor(private route: ActivatedRoute, private employeeApiService: EmployeeAPIService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.rolesId = Number(params.get('id'));
      if (this.rolesId !== null) {
        this.employeeApiService.getEmployeesByRole(this.rolesId).subscribe(data => {
          this.employees = data;
        });
      }
    });
  }
}
