export interface ColumnItem {
  name: string;
  key: string;
  listOfFilter?: Array<{ text: string; value: string }>;
  width?: string;
}
