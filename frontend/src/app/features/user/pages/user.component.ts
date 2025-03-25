import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserDataGridComponent } from '../components/user-data-grid/user-data-grid.component';

@Component({
  selector: 'feature-user-page',
  standalone: true,
  imports: [
    UserDataGridComponent
  ],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent {
}
