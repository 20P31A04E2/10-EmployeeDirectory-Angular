import { Component, OnInit, HostListener } from '@angular/core';
import { EmployeeAPIService, Employee, Location, Department } from '../employee-api.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main-content',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './main-content.component.html',
  styleUrl: './main-content.component.css'
})

export class MainContentComponent implements OnInit {

  buttonList: Button[] = [];

  departments = [
    { value: 'Full stack', key: 'Full stack' },
    { value: 'Quality anlaysis', key: 'Quality anlaysis' },
    { value: 'Data analytics', key: 'Data analytics' },
    { value: 'UI Development', key: 'UI Development' }
  ]

  tableHeader = [
    { value: "USERS", key: "user" },
    { value: "LOCATION", key: "location" },
    { value: "DEPARTMENT", key: "department" },
    { value: "ROLE", key: "role" },
    { value: "EMP.NO", key: "empno" },
    { value: "STATUS", key: "status" },
    { value: "JOIN DT", key: "joindt" }
  ];

  allLocations: Location[] = [];
  allDepartments: Department[] = [];
  employees: Employee[] = [];

  constructor(private employeeAPIService: EmployeeAPIService, private router: Router) { }

  ngOnInit(): void {

    this.generateButtonList();

    this.employeeAPIService.getLocations().subscribe(data => {
      this.allLocations = data;
    })

    this.employeeAPIService.getDepartments().subscribe(data => {
      this.allDepartments = data;
    })

    this.employeeAPIService.getEmployees().subscribe(data => {
      this.employees = data;
    });

  }

  // Button generation
  generateButtonList() {
    for (let i = 65; i <= 90; i++) {
      const char = String.fromCharCode(i);
      this.buttonList.push({
        value: char,
        key: char.toLowerCase(),
        backgroundColor: '',
        fontColor: ''
      });
    }
  }

  //Button filtering
  private previousButton: any = null;
  FilterButtonText: string = '';
  buttonFiltering(button: any): void {
    if (this.previousButton) {
      this.previousButton.backgroundColor = 'rgb(243, 242, 241)';
      this.previousButton.fontColor = 'black';
    }
    this.previousButton = button;
    button.backgroundColor = 'red';
    button.fontColor = 'white';
    this.FilterButtonText = button.key;
    this.filteringAndSorting();
  }

  //Multiselect filtering
  dropdownStates = {
    statusDropdownVisible: false,
    locationDropdownVisible: false,
    departmentDropdownVisible: false
  };

  toggleDropdown(dropdown: keyof typeof this.dropdownStates) {
    this.dropdownStates[dropdown] = !this.dropdownStates[dropdown];
  }

  @HostListener('document:click', ['$event'])
  handleClick(event: Event) {
    const targetElement = event.target as HTMLElement;

    const statusDropdown = document.querySelector('.multiselect-dropdown[data-filter="Status"] .multiselect-dropdown-content');
    const locationDropdown = document.querySelector('.multiselect-dropdown[data-filter="Location"] .multiselect-dropdown-content');
    const departmentDropdown = document.querySelector('.multiselect-dropdown[data-filter="Department"] .multiselect-dropdown-content');

    if (statusDropdown && this.dropdownStates.statusDropdownVisible && !targetElement.closest('.multiselect-dropdown[data-filter="Status"]')) {
      this.dropdownStates.statusDropdownVisible = false;
    }

    if (locationDropdown && this.dropdownStates.locationDropdownVisible && !targetElement.closest('.multiselect-dropdown[data-filter="Location"]')) {
      this.dropdownStates.locationDropdownVisible = false;
    }

    if (departmentDropdown && this.dropdownStates.departmentDropdownVisible && !targetElement.closest('.multiselect-dropdown[data-filter="Department"]')) {
      this.dropdownStates.departmentDropdownVisible = false;
    }
  }

  statusCheckedValues: { [key: string]: boolean } = {};
  statusSelectedValues: string[] = [];
  locationCheckedValues: { [key: string]: boolean } = {};
  locationSelectedValues: string[] = [];
  departmentCheckedValues: { [key: string]: boolean } = {};
  departmentSelectedValues: string[] = [];

  onStatusCheckboxChange(value: string, event: any): void {
    if (event.target.checked) {
      this.statusSelectedValues.push(value);
    } else {
      const index = this.statusSelectedValues.indexOf(value);
      if (index > -1) {
        this.statusSelectedValues.splice(index, 1);
      }
    }
    console.log(this.statusSelectedValues);

  }

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
    return this.statusSelectedValues.length > 0 || this.locationSelectedValues.length > 0 || this.departmentSelectedValues.length > 0;
  }

  // Sorting based on column header
  sortBy: string = "";
  sortOrder: string = "";

  onSortChange(column: string) {
    if (this.sortBy === column) {
      this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortBy = column;
      this.sortOrder = 'asc';
    }
    this.filteringAndSorting();
    console.log(this.sortBy + " " + this.sortOrder);
  }

  filteringAndSorting() {
    this.employeeAPIService.FilteredAndSortedEmployees(this.FilterButtonText, this.statusSelectedValues, this.locationSelectedValues, this.departmentSelectedValues, this.sortBy, this.sortOrder).subscribe(data => {
      this.employees = data;
    });
  }


  //Filter reset
  filterReset(): void {
    if (this.previousButton) {
      this.previousButton.backgroundColor = 'rgb(243, 242, 241)';
      this.previousButton.fontColor = 'black';
    }
    this.employeeAPIService.getEmployees().subscribe(data => {
      this.employees = data;
    });

    this.statusCheckedValues = {};
    this.statusSelectedValues = [];
    this.locationCheckedValues = {};
    this.locationSelectedValues = [];
    this.departmentCheckedValues = {};
    this.departmentSelectedValues = [];
    this.sortBy = "";
    this.sortOrder = "";
    this.FilterButtonText = "";
  }

  // Deleting employees
  selectedEmployeeIds: string[] = [];
  isDeleteButtonEnabled: boolean = false;

  toggleSelection(employeeId: string): void {
    if (this.isSelected(employeeId)) {
      this.selectedEmployeeIds = this.selectedEmployeeIds.filter(id => id !== employeeId);
    } else {
      this.selectedEmployeeIds.push(employeeId);
      this.isDeleteButtonEnabled = true;
    }
    this.updateAllCheckedState();
  }

  isSelected(employeeId: string): boolean {
    return this.selectedEmployeeIds.includes(employeeId);
  }

  deleteSelectedEmployees(): void {
    this.employeeAPIService.employeeIdToDelete(this.selectedEmployeeIds).subscribe(
      () => {
        this.employeeAPIService.getEmployees().subscribe(data => {
          this.employees = data;
        });
        this.selectedEmployeeIds = [];
      }
    )
  }

  // selecting rows using thead checkbox
  allChecked = false;
  toggleAllSelection(event: Event): void {
    const checked = (event.target as HTMLInputElement).checked;
    this.allChecked = checked;
    if (checked) {
      this.selectedEmployeeIds = this.employees.map(emp => emp.employeeId);
    } else {
      this.selectedEmployeeIds = [];
    }
  }

  updateAllCheckedState(): void {
    this.allChecked = this.employees.length > 0 && this.employees.every(emp => this.selectedEmployeeIds.includes(emp.employeeId));
  }

  // Edit and Delete emp using ellipsis
  selectEmployeeToDelete(employeeId: string, event: Event) {
    const target = event.target as HTMLSelectElement;
    const value = target.value;
    if (value === 'Edit') {
      this.router.navigate(['/employees/edit', employeeId]);
    } else if (value === 'Delete') {
      this.selectedEmployeeIds.push(employeeId);
      this.deleteSelectedEmployees();
    }
  }

}

interface Button {
  value: string;
  key: string;
  backgroundColor: string;
  fontColor: string;
}
