import { Component, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EmployeeAPIService, Location, Department, Role } from '../employee-api.service';
import { RouterLink, Router } from '@angular/router';

@Component({
  selector: 'app-roles-content',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './roles-content.component.html',
  styleUrl: './roles-content.component.css'
})
export class RolesContentComponent {
  allLocations: Location[] = [];
  allDepartments: Department[] = [];
  allRoles: Role[] = [];
  
  constructor(private employeeAPIService: EmployeeAPIService, private router: Router) { }

  ngOnInit(): void {
    this.employeeAPIService.getLocations().subscribe(data => {
      this.allLocations = data;
    })

    this.employeeAPIService.getDepartments().subscribe(data => {
      this.allDepartments = data;
    })

    this.employeeAPIService.getRoles().subscribe(data => {
      this.allRoles = data;
    })

 }

  //Multiselect filtering
  dropdownStates = {
    locationDropdownVisible: false,
    departmentDropdownVisible: false
  };

  toggleDropdown(dropdown: keyof typeof this.dropdownStates) {
    this.dropdownStates[dropdown] = !this.dropdownStates[dropdown];
  }

  @HostListener('document:click', ['$event'])
  handleClick(event: Event) {
    const targetElement = event.target as HTMLElement;

    const locationDropdown = document.querySelector('.multiselect-dropdown[data-filter="Location"] .multiselect-dropdown-content');
    const departmentDropdown = document.querySelector('.multiselect-dropdown[data-filter="Department"] .multiselect-dropdown-content');

    if (locationDropdown && this.dropdownStates.locationDropdownVisible && !targetElement.closest('.multiselect-dropdown[data-filter="Location"]')) {
      this.dropdownStates.locationDropdownVisible = false;
    }

    if (departmentDropdown && this.dropdownStates.departmentDropdownVisible && !targetElement.closest('.multiselect-dropdown[data-filter="Department"]')) {
      this.dropdownStates.departmentDropdownVisible = false;
    }
  }

  locationCheckedValues: { [key: string]: boolean } = {};
  locationSelectedValues: string[] = [];
  departmentCheckedValues: { [key: string]: boolean } = {};
  departmentSelectedValues: string[] = [];

  onLocationCheckboxChange(value: string, event: any): void {
    if (event.target.checked) {
      this.locationSelectedValues.push(value);
    } else {
      const index = this.locationSelectedValues.indexOf(value);
      if (index > -1) {
        this.locationSelectedValues.splice(index, 1);
      }
    }
    console.log(this.locationSelectedValues);
  }

  onDepartmentCheckboxChange(value: string, event: any): void {
    if (event.target.checked) {
      this.departmentSelectedValues.push(value);
    } else {
      const index = this.departmentSelectedValues.indexOf(value);
      if (index > -1) {
        this.departmentSelectedValues.splice(index, 1);
      }
    }
  }

  isAnyCheckboxChecked(): boolean {
    return this.locationSelectedValues.length > 0 || this.departmentSelectedValues.length > 0;
  }

  filterReset(): void {
    this.locationCheckedValues = {};
    this.locationSelectedValues = [];
    this.departmentCheckedValues = {};
    this.departmentSelectedValues = [];

    this.employeeAPIService.getRoles().subscribe(data => {
      this.allRoles = data;
    })
  }

  filtering() {
    this.employeeAPIService.getFilteredRoles(this.locationSelectedValues, this.departmentSelectedValues).subscribe(data => {
      this.allRoles = data;
    })
  }

  viewEmployees(rolesId: number| undefined): void {
    this.router.navigate(['/role', rolesId, 'employees']);
  }

}