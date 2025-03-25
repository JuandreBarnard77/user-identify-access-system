import { Component, Input } from '@angular/core';
import { DxDataGridModule } from 'devextreme-angular';
import { defaultDataGridProperties } from './data-grid-default-properties';
import { ColumnProperties } from './properties/column';
import DevExpress from 'devextreme';
import GridsEditMode = DevExpress.common.grids.GridsEditMode;

@Component({
  selector: 'app-data-grid',
  standalone: true,
  imports: [
    DxDataGridModule,
  ],
  templateUrl: './data-grid.component.html',
  styleUrl: './data-grid.component.css'
})
export class DataGridComponent {
  @Input() public id = "";
  @Input({ required: true }) public dataSource!: any;
  @Input() public keyExpr: string | undefined;
  @Input({ required: true }) public columns!: ColumnProperties[];
  @Input() public gridProperties = defaultDataGridProperties;
  @Input() public editType: GridsEditMode = 'popup';
  @Input() public allowUpdating = true;
  @Input() public allowDeleting = true;
  @Input() public allowAdding = true;
}
