export class HistoryFilterRequest {
  page: number;
  pageSize: number;

  // Filters
  id?: string;
  text?: string;
  name?: string;
  eventType?: number;
  dateFrom?: string;
  dateTo?: string;

  // Sorting
  sort?: SortField[];
}

export class SortField {
  field!: string;
  order!: string;
}
