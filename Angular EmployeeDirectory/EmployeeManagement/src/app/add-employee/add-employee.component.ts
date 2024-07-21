import { Component, OnInit } from '@angular/core';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { EmployeeAPIService, Employee, Location, Department, Role, Project } from '../employee-api.service';
import { ReactiveFormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-add-employee',
  standalone: true,
  imports: [RouterLink, CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css'],
  providers: [DatePipe]
})
export class AddEmployeeComponent implements OnInit {
  allLocations: Location[] = [];
  filteredRoles: Role[] = [];
  filteredDepartments: Department[] = [];
  managersList: Employee[] = [];
  projectsList: Project[] = [];
  employees: Employee[] = [];
  selectedLocationId: number = 0;
  selectedRoleId: number = 0;
  searchQueryManager: string = '';
  showManagerDropdown: boolean = false;
  selectedManagers: string[] = [];
  searchQueryProject: string = '';
  showProjectDropdown: boolean = false;
  selectedProjects: string[] = [];
  isFormValid: boolean = false;
  showAlert: boolean = false;
  selectedProjectName: string = '';
  projectId: number | null = 0;
  employeeId: string = '';
  addEmployeeForm!: FormGroup;
  isEditMode: boolean = false;
  isInitializingForm: boolean = false;

  constructor(private employeeAPIService: EmployeeAPIService, private router: Router, private datePipe: DatePipe, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.employeeAPIService.getLocations().subscribe(data => {
      this.allLocations = data;
    });

    this.route.params.subscribe(params => {
      this.employeeId = params['id'];
      if (this.employeeId) {
        this.employeeAPIService.getEmployeeById(this.employeeId).subscribe(employee => {
          this.isEditMode = true;
          this.initForm(employee);
        });
      } else {
        this.initForm();
      }
    });
  }

  onLocationChange(event: Event) {
    if (this.isInitializingForm) {
      return;
    }
    const selectElement = event.target as HTMLSelectElement;
    const locationId = Number(selectElement.value);
    this.selectedLocationId = locationId;
    this.employeeAPIService.getRolesByLocation(locationId).subscribe(roles => {
      this.filteredRoles = roles;
    });
  }

  onRoleChange(event: Event) {
    if (this.isInitializingForm) {
      return;
    }
    const selectElement = event.target as HTMLSelectElement;
    const roleId = Number(selectElement.value);
    this.selectedRoleId = roleId;
    this.employeeAPIService.getDepartmentsByRole(roleId).subscribe(departments => {
      this.filteredDepartments = departments;
    });
  }

  onDepartmentChange(event: Event) {
    if (this.isInitializingForm) {
      return;
    }
    const selectElement = event.target as HTMLSelectElement;
    const departmentId = Number(selectElement.value);
    this.employeeAPIService.getManagersByDepartment(departmentId).subscribe(employees => {
      this.managersList = employees;
    });
    this.employeeAPIService.getProjectsByDepartment(departmentId).subscribe(projects => {
      this.projectsList = projects;
    });
  }

  get filteredManagers() {
    if (this.searchQueryManager === '') {
      return this.managersList;
    }
    return this.managersList.filter(manager =>
      manager.firstName.toLowerCase().includes(this.searchQueryManager.toLowerCase()));
  }

  get filteredProjects() {
    if (this.searchQueryProject === '') {
      return this.projectsList;
    }
    return this.projectsList.filter(project =>
      project.projectName.toLowerCase().includes(this.searchQueryProject.toLowerCase()));
  }

  onManagerFocus() {
    this.showManagerDropdown = true;
  }

  onManagerBlur() {
    setTimeout(() => {
      this.showManagerDropdown = false;
    }, 500);
  }

  onProjectFocus() {
    this.showProjectDropdown = true;
  }

  onProjectBlur() {
    setTimeout(() => {
      this.showProjectDropdown = false;
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

  toggleProjectSelection(projectName: string) {
    const index = this.selectedProjects.indexOf(projectName);
    if (index > -1) {
      this.selectedProjects.splice(index, 1);
    } else {
      this.selectedProjects.push(projectName);
    }
    this.updateSearchQueryProject();
  }

  updateSearchQueryProject() {
    this.searchQueryProject = this.selectedProjects.join(', ');
    this.selectedProjectName = this.selectedProjects[0];
    this.projectId = this.getProjectId(this.selectedProjectName);
    this.onProjectBlur();
  }

  onManagerInputChange(event: any) {
    const inputValue = event.target.value;
    if (inputValue === '') {
      this.selectedManagers = [];
    }
  }

  onProjectInputChange(event: any) {
    const inputValue = event.target.value;
    if (inputValue === '') {
      this.selectedProjects = [];
    }
  }

  getProjectId(selectedProjectName: string): number | null {
    const selectedProject = this.projectsList.find(project => project.projectName === selectedProjectName);
    return selectedProject ? selectedProject.projectId : null;
  }

  initForm(employee?: Employee): void {
    const employeeIdPattern = /^[T][Z]\d{4}$/;
    this.isInitializingForm = true;

    this.addEmployeeForm = new FormGroup({
      employeeId: new FormControl(employee ? employee.employeeId : '', [Validators.required, Validators.pattern(employeeIdPattern)]),
      firstName: new FormControl(employee ? employee.firstName : '', [Validators.required, Validators.minLength(5)]),
      lastName: new FormControl(employee ? employee.lastName : '', [Validators.required, Validators.minLength(5)]),
      dateOfBirth: new FormControl(employee ? this.datePipe.transform(employee.dateOfBirth, 'yyyy-MM-dd') : '', [Validators.required]), // Convert date to proper format if needed
      email: new FormControl(employee ? employee.email : '', [Validators.required, Validators.email]),
      phone: new FormControl(employee ? employee.phone : '', [Validators.minLength(10), Validators.maxLength(10)]),
      joinDate: new FormControl(employee ? this.datePipe.transform(employee.joinDate, 'yyyy-MM-dd') : '', [Validators.required]), // Convert date to proper format if needed
      locationName: new FormControl(employee ? employee.locationId : '', [Validators.required]),
      roleName: new FormControl(employee ? employee.roleId : '', [Validators.required]),
      departmentName: new FormControl(employee ? employee.departmentId : '', [Validators.required]),
      manager: new FormControl(employee ? employee.manager : '', Validators.required),
      projectName: new FormControl(employee ? employee.projectId : '', Validators.required)
    });

    if (employee) {
      this.selectedLocationId = employee.locationId;
      this.selectedRoleId = employee.roleId;
      this.employeeAPIService.getRolesByLocation(this.selectedLocationId).subscribe(roles => {
        this.filteredRoles = roles;
        this.employeeAPIService.getDepartmentsByRole(this.selectedRoleId).subscribe(departments => {
          this.filteredDepartments = departments;
          this.employeeAPIService.getManagersByDepartment(employee.departmentId).subscribe(managers => {
            this.managersList = managers;
            this.employeeAPIService.getProjectsByDepartment(employee.departmentId).subscribe(projects => {
              this.projectsList = projects;
              this.isInitializingForm = false;
            });
          });
        });
      });
    } else {
      this.isInitializingForm = false;
    }
  }

  onSubmit(): void {
    this.isFormValid = true;

    if (this.addEmployeeForm.valid) {
      const formValues = this.addEmployeeForm.value;
      const newEmployee: Employee = {
        employeeId: formValues.employeeId || " ",
        firstName: formValues.firstName || " ",
        lastName: formValues.lastName || " ",
        dateOfBirth: this.datePipe.transform(formValues.dateOfBirth, 'yyyy-MM-dd') || '',
        email: formValues.email || " ",
        phone: formValues.phone || " ",
        joinDate: this.datePipe.transform(formValues.joinDate, 'yyyy-MM-dd') || '',
        locationId: Number(formValues.locationName) || 0,
        roleId: Number(formValues.roleName) || 0,
        departmentId: Number(formValues.departmentName) || 0,
        manager: formValues.manager || " ",
        projectId: this.projectId || 0,
        status: "Active"
      };

      if (this.isEditMode) {
        this.employeeAPIService.updateEmployee(this.employeeId, newEmployee).subscribe({
          next: response => {
            console.log('Employee updated successfully:', response);
            this.showAlert = true;
            setTimeout(() => {
              this.showAlert = false;
              this.router.navigate(['/']);
            }, 2000);
          },
          error: error => {
            this.handleError(error);
          }
        });
      } else {
        this.employeeAPIService.addingEmployee(newEmployee).subscribe({
          next: response => {
            console.log('Employee added successfully:', response);
            this.showAlert = true;
            setTimeout(() => {
              this.showAlert = false;
              this.router.navigate(['/']);
            }, 2000);
          },
          error: error => {
            this.handleError(error);
          }
        });
      }
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
