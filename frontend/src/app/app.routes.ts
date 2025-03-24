import { Routes } from '@angular/router';
import { GroupComponent } from './features/group/group.component';
import { UserComponent } from './features/user/pages/user.component';
import { PermissionComponent } from './features/permission/permission.component';

export const routes: Routes = [
  { path: '', redirectTo: '/users', pathMatch: 'full' },
  { path: 'users', component: UserComponent },
  { path: 'groups', component: GroupComponent },
  { path: 'permissions', component: PermissionComponent }
];
