import { Routes } from '@angular/router';
import { MainContentComponent} from './main-content/main-content.component';
import { AddEmployeeComponent } from './add-employee/add-employee.component';
import { RolesContentComponent } from './roles-content/roles-content.component';
import { AddRoleComponent } from './add-role/add-role.component';
import { EmployeesInARoleComponent } from './employees-in-arole/employees-in-arole.component';

export const routes: Routes = [
    {path:'',component:MainContentComponent},
    {path:'addEmployee',component:AddEmployeeComponent},
    {path: 'employees/edit/:id', component: AddEmployeeComponent },
    {path: 'roles' , component: RolesContentComponent},
    {path: 'addRole', component: AddRoleComponent},
    {path: 'role/:id/employees', component: EmployeesInARoleComponent}
];
