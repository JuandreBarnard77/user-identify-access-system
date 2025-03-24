import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DataGridComponent } from './data-grid.component';
import { DxDataGridModule } from 'devextreme-angular';
import { defaultDataGridProperties } from './data-grid-default-properties';
import { By } from '@angular/platform-browser';

describe('GridComponent', () => {
  let component: DataGridComponent;
  let fixture: ComponentFixture<DataGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DxDataGridModule, DataGridComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DataGridComponent);
    component = fixture.componentInstance;

    // Set input values
    component.dataSource = [{ id: 1, name: 'Test Item' }];
    component.keyExpr = 'id';
    component.columns = [
      { dataField: 'id', caption: 'ID', dataType: 'number', visibleIndex: 0 },
      { dataField: 'name', caption: 'Name', dataType: 'string', visibleIndex: 1 }
    ];
    component.gridProperties = defaultDataGridProperties;
    component.editType = 'popup';
    component.allowUpdating = true;
    component.allowDeleting = true;
    component.allowAdding = true;

    fixture.detectChanges(); // Trigger change detection
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should have the correct inputs bound', () => {
    expect(component.dataSource).toEqual([{ id: 1, name: 'Test Item' }]);
    expect(component.keyExpr).toBe('id');
    expect(component.columns.length).toBe(2);
    expect(component.allowUpdating).toBeTrue();
    expect(component.allowDeleting).toBeTrue();
    expect(component.allowAdding).toBeTrue();
  });

  it('should render dx-data-grid with the correct dataSource', () => {
    fixture.detectChanges();
    const gridElement = fixture.debugElement.query(By.css('dx-data-grid'));
    expect(gridElement).toBeTruthy();
  });

  it('should pass keyExpr to dx-data-grid', () => {
    const dataGrid = fixture.debugElement.query(By.css('dx-data-grid')).componentInstance;
    expect(dataGrid.keyExpr).toBe('id');
  });

  it('should have column configurations set', () => {
    fixture.detectChanges();
    const gridComponent = fixture.debugElement.query(By.css('dx-data-grid')).componentInstance;
    expect(gridComponent.columns.length).toBe(2);
    expect(gridComponent.columns[0].dataField).toBe('id');
    expect(gridComponent.columns[1].dataField).toBe('name');
  });

  it('should have editing options enabled', () => {
    fixture.detectChanges();
    const gridComponent = fixture.debugElement.query(By.css('dx-data-grid')).componentInstance;
    expect(gridComponent.editing.allowUpdating).toBeTrue();
    expect(gridComponent.editing.allowDeleting).toBeTrue();
    expect(gridComponent.editing.allowAdding).toBeTrue();
  });
});
