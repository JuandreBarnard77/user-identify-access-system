import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { DxDrawerModule, DxListModule, DxToolbarModule } from 'devextreme-angular';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, DxDrawerModule, DxToolbarModule, DxListModule, RouterLink],
  templateUrl: './app.component.html',
  standalone: true,
  styleUrl: './app.component.css'
})
export class AppComponent {
  toolbarContent = [{
    widget: 'dxButton',
    location: 'before',
    options: {
      icon: 'menu',
      stylingMode: 'text',
      onClick: () => this.isDrawerOpen = !this.isDrawerOpen,
    },
  }];

  title = 'uai-management';
  navigation: any[] = [
    { id: 1, text: "User", icon: "user", path: "inbox" },
    // { id: 2, text: "Group", icon: "group", path: "sent-mail" },
    // { id: 3, text: "Permission", icon: "key", path: "trash" }
  ];
  isDrawerOpen: boolean = true;
}
