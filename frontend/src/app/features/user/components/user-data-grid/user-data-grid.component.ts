import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { DataGridComponent } from '../../../../shared/components/data-grid/data-grid.component';
import { User } from '../../data-access/dtos/user';
import { ColumnProperties } from '../../../../shared/components/data-grid/properties/column';
import { firstValueFrom, Subject, takeUntil } from 'rxjs';
import { UserService } from '../../data-access/services/user.service';
import CustomStore from 'devextreme/data/custom_store';

export const userColumnProperties: ColumnProperties[] = [
  {
    dataField: "id",
    dataType: 'number',
    caption: 'ID',
    visible: true,
    showInColumnChooser: true,
    allowEditing: false,
    visibleIndex: 1,
    width: '50px'
  },
  {
    dataField: "firstname",
    dataType: 'string',
    caption: 'Firstname',
    visible: true,
    showInColumnChooser: true,
    visibleIndex: 2,
  },
  {
    dataField: 'lastname',
    dataType: 'string',
    caption: 'Lastname',
    visible: true,
    showInColumnChooser: true,
    visibleIndex: 3
  },
  {
    dataField: 'email',
    dataType: 'string',
    caption: 'Email',
    visible: true,
    showInColumnChooser: true,
    visibleIndex: 3
  }
];

@Component({
  selector: 'user-data-grid',
  standalone: true,
  imports: [
    DataGridComponent,
  ],
  templateUrl: './user-data-grid.component.html',
  styleUrl: './user-data-grid.component.css'
})
export class UserDataGridComponent implements OnInit, OnDestroy {
  public properties = userColumnProperties;
  private readonly _onDestroy$: Subject<void> = new Subject();
  public dataSource!: CustomStore;

  constructor(private userService: UserService) { }

  public ngOnInit(): void {
    this.dataSource = new CustomStore({
      key: 'id',
      load: async (loadOptions) => {
        try {
          const response = await firstValueFrom(this.userService.getAllUsers());
          return {
            data: response,
            totalCount: response.length,
          };
        } catch (error) {
          console.error('Load error:', error);
          throw error;
        }},
      insert: (values) =>  firstValueFrom(this.userService.addUser(values)),
      update: async (key, values) => {
        try {

          const originalObject = await firstValueFrom(this.userService.getUserById(key));
          const updatedObject = { ...originalObject, ...values };
          return firstValueFrom(this.userService.editUser(updatedObject));
        } catch (error) {
          console.error('Update error:', error);
          throw error; // Notify DataGrid of failure
        }
      },
      remove: (key) => firstValueFrom(this.userService.deleteUser(key))
    });
  }

  public ngOnDestroy(): void {
    this._onDestroy$.next();
    this._onDestroy$.unsubscribe();
  }
}
