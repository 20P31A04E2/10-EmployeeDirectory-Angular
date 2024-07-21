import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, forkJoin } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeAPIService {
  private employeeApiUrl = 'https://localhost:7051/api/Employees';
  private locationsApiUrl = 'https://localhost:7051/api/Locations';
  private departmentsApiUrl = 'https://localhost:7051/api/Departments';
  private rolesApiUrl = 'https://localhost:7051/api/Roles';
  private projectsApiUrl = 'https://localhost:7051/api/Projects';

  constructor(private http:HttpClient) { }

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.employeeApiUrl);
  }

  getEmployeeById(employeeId: string): Observable<Employee> {
    return this.http.get<Employee>(`${this.employeeApiUrl}/${employeeId}`);
  }

  getEmployeesByRole(rolesId: number): Observable<Employee[]>{
    return this.http.get<Employee[]>(`${this.employeeApiUrl}/role/${rolesId}`);
  }

  addingEmployee(employee: Employee): Observable<Employee> {
    return this.http.post<Employee>(this.employeeApiUrl, employee);
  }

  addingRole(role: Role):Observable<Role> {
    return this.http.post<Role>(this.rolesApiUrl, role);
  }

  updateEmployee(id: string, employee: Employee): Observable<Employee> {
    return this.http.put<Employee>(`${this.employeeApiUrl}/${id}`, employee);
  }

  getLocations(): Observable<Location[]> {
    return this.http.get<Location[]>(this.locationsApiUrl);
  }

  getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(this.departmentsApiUrl);
  }

  getRoles(): Observable<Role[]>{
    return this.http.get<Role[]>(this.rolesApiUrl);
  }

  getFilteredRoles(locationSelectedValues: string[],departmentSelectedValues:string[]):Observable<Role[]>{
    const roleFilterCriteria ={Locations:locationSelectedValues, Departments:departmentSelectedValues};
    return this.http.post<Role[]>(`${this.rolesApiUrl}/RolesFiltering`, roleFilterCriteria);
  }

  getRolesByLocation(locationId: number): Observable<Role[]> {
    return this.http.get<Role[]>(`${this.rolesApiUrl}/GetRolesByLocation/${locationId}`);
  }

  getDepartmentsByRole(roleId: number): Observable<Department[]> {
    return this.http.get<Department[]>(`${this.departmentsApiUrl}/GetDepartmentsByRole/${roleId}`);
  }

  getManagersByDepartment(departmentId: number): Observable<Employee[]>{
    return this.http.get<Employee[]>(`${this.employeeApiUrl}/GetEmployeesByDepartment/${departmentId}`);

  }
  getProjectsByDepartment (departmentId : number): Observable<Project[]>{
    return this.http.get<Project[]>(`${this.projectsApiUrl}/GetProjectsByDepartment/${departmentId}`);
  }

  FilteredAndSortedEmployees(FilterButtonText:string,statusSelectedValues:string[],locationSelectedValues: string[],departmentSelectedValues:string[],sortBy: string, sortOrder: string):Observable<Employee[]>{
    const filterCriteria = {FilterButtonText:FilterButtonText,Status:statusSelectedValues,Locations:locationSelectedValues, Departments:departmentSelectedValues, SortBy:sortBy, SortOrder: sortOrder};
    return this.http.post<Employee[]>(`${this.employeeApiUrl}/FilteringAndSorting`, filterCriteria);
  }

  employeeIdToDelete(employeeIds: string[]): Observable<void[]> {
    const deleteRequests: Observable<void>[] = [];
    employeeIds.forEach(id => {
      const request = this.http.delete<void>(`${this.employeeApiUrl}/${id}`);
      deleteRequests.push(request);
    });

    // Combine all delete requests into a single observable
    return forkJoin(deleteRequests);
  }

}

export interface Employee{
  employeeId:string
  firstName:string,
  lastName:string,
  dateOfBirth:Date | string,
  email:string,
  phone:string,
  joinDate:Date | string,
  locationId:number,
  locationName?:string,
  roleId:number,
  roleName?:string,
  departmentId:number,
  departmentName?:string,
  manager:string,
  projectId:number,
  projectName?:string,
  status?:string
}

export interface Location{
  locationId:number,
  locationName:string,
}

export interface Department{
  departmentId: number,
  departmentName: string
}

export interface Project{
  projectId:number,
  projectName:string,
  departmentId:number
}

export interface Role{
  rolesId?:number,
  roleName:string,
  roleDescription:string,
  locationId:number,
  locationName?:string,
  departmentId:number,
  departmentName?:string
}
