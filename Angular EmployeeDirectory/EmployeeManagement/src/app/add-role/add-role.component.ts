import { Component } from '@angular/core';
import { EmployeeAPIService, Employee, Department, Location, Role } from '../employee-api.service';
import { CommonModule } from '@angular/common';
import { RouterLink,Router } from '@angular/router';
import { FormsModule, ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';


@Component({
  selector: 'app-add-role',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, ReactiveFormsModule],
  templateUrl: './add-role.component.html',
  styleUrl: './add-role.component.css'
})
export class AddRoleComponent {

  allDepartments: Department[] = [];
  allLocations: Location[] = [];
  searchQueryManager: string = '';
  employeesList: Employee[] = [];
  showManagerDropdown: boolean = false;
  selectedManagers: string[] = [];
  isFormValid: boolean = false;
  showAlert: boolean = false;


  constructor(private employeeAPIService: EmployeeAPIService,private router: Router) { }

  ngOnInit(): void {
    this.employeeAPIService.getDepartments().subscribe(data => {
      this.allDepartments = data;
    })

    this.employeeAPIService.getLocations().subscribe(data => {
      this.allLocations = data;
    })

    this.employeeAPIService.getEmployees().subscribe(data => {
      this.employeesList = data;
    })
  }

  onManagerFocus() {
    this.showManagerDropdown = true;
  }

  get filteredManagers() {
    if (this.searchQueryManager === '') {
      return this.employeesList;
    }
    return this.employeesList.filter(manager =>
      manager.firstName.toLowerCase().includes(this.searchQueryManager.toLowerCase()));
  }

  onManagerBlur() {
    setTimeout(() => {
      this.showManagerDropdown = false;
    }, 500);
  }

  toggleManagerSelection(managerName: string) {
    const index = this.selectedManagers.indexOf(managerName);
    if (index > -1) {
      this.selectedManagers.splice(index, 1);
    } else {
      this.selectedManagers.push(managerName);
    }
    this.updateSearchQueryManager();
  }

  updateSearchQueryManager() {
    this.searchQueryManager = this.selectedManagers.join(', ');
    this.onManagerBlur();
  }

  onManagerInputChange(event: any) {
    const inputValue = event.target.value;
    if (inputValue === '') {
      this.selectedManagers = [];
    }
  }


  addRoleForm = new FormGroup({
    roleName: new FormControl('', Validators.required),
    departmentName: new FormControl('', [Validators.required]),
    roleDescription: new FormControl('', [Validators.required, Validators.minLength(5)]),
    locationName: new FormControl('', [Validators.required]),
  });

  onSubmit(): void {
    this.isFormValid = true;
    if (this.addRoleForm.valid) {
      const formValues = this.addRoleForm.value;

      const newRole: Role = {
        roleName: formValues.roleName || " ",
        roleDescription: formValues.roleDescription || " ",
        locationId: Number(formValues.locationName) || 0,
        departmentId: Number(formValues.departmentName) || 0,
      };
      console.log(newRole);
      this.employeeAPIService.addingRole(newRole).subscribe({
        next: response => {
          console.log('Role added successfully:', response);
          this.showAlert = true;
            setTimeout(() => {
              this.showAlert = false;
              this.router.navigate(['/roles']);
            }, 2000);
        },
        error: error => {
          this.handleError(error);
        }
      });
    }
  }

  private handleError(error: HttpErrorResponse): void {
    console.error('Error:', error);
    if (error.status === 400 && error.error && error.error.errors) {
      const validationErrors = error.error.errors;
      console.error('Validation errors:', validationErrors);
    } else {
      console.error('Other error:', error.statusText);
    }
  }
}
