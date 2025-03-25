export interface ColumnProperties {
  dataField: string;
  dataType?: "string" | "number" | "date" | "boolean" | "object" | "datetime" | undefined;
  caption?: string;
  visible?: boolean;
  showInColumnChooser?: boolean;
  allowSorting?: boolean;
  allowFiltering?: boolean;
  allowGrouping?: boolean;
  allowSearch?: boolean;
  allowEditing?: boolean;
  allowHeaderFiltering?: boolean;
  visibleIndex?: number;
  format?: string;
  width?: number | string;
  type?:  "adaptive" | "buttons" | "detailExpand" | "groupExpand" | "selection" | "drag";
};
