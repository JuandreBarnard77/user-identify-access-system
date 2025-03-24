import { Component } from '@angular/core';

@Component({
  selector: 'feature-user',
  standalone: true,
  imports: [],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent {
  public user: User;
}
