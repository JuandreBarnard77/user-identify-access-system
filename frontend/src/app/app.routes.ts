import { Routes } from '@angular/router';
import { UserComponent } from './features/user/pages/user.component';

export const routes: Routes = [
  { path: '', redirectTo: '/users', pathMatch: 'full' },
  { path: 'users', component: UserComponent }
];
