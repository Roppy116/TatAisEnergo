import { Component, OnInit } from '@angular/core';
import { NzTableModule, NzTableQueryParams } from 'ng-zorro-antd/table';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { FormsModule } from '@angular/forms';
import { HistoryModel } from '../../shared/models/history.model';
import { EventService } from '../../api/services/event.service';
import { DatePipe } from '@angular/common';
import { NzResizableModule } from 'ng-zorro-antd/resizable';
import { HistoryFilterRequest } from '../../shared/models/filter/history.filter';
import { ColumnItem } from '../../shared/interfaces/columnItem';

@Component({
  selector: 'nz-demo-table-sort-filter',
  standalone: true,
  imports: [
    NzTableModule,
    DatePipe,
    NzInputModule,
    NzDatePickerModule,
    NzButtonModule,
    NzIconModule,
    NzDropDownModule,
    FormsModule,
    NzResizableModule,
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  eventTypes: Map<number, string> = new Map<number, string>();
  filter: HistoryFilterRequest = new HistoryFilterRequest();

  listOfColumns: ColumnItem[] = [
    { name: 'Id', key: 'id', width: '100px' },
    { name: 'Text', key: 'text', width: '200px' },
    { name: 'Name', key: 'name', width: '200px'},
    { name: 'Date', key: 'date', width: '200px' },
    { name: 'Event', key: 'eventType', width: '150px' }
  ];

  listOfData: HistoryModel[] = [];
  pageIndex: number = 1;
  pageSize: number = 10;
  total: number = 0;

  searchValue: { [key: string]: string } = {};
  dateRange: { [key: string]: Date[] } = {};
  sortFields: { field: string; order: string }[] = [];

  constructor(private eventService: EventService) {}

  ngOnInit(): void {
    this.loadEventTypes();
  }

  loadTableData(): void {
    const t = this;
    const params = {
      page: t.pageIndex,
      pageSize: t.pageSize,
      ...t.getFiltersForRequest(),
      ...t.getSortForRequest()
    };

    t.filter.page = params.page;
    t.filter.pageSize = params.pageSize;
    t.filter.id = params.id;
    t.filter.text = params.text?.trim();
    t.filter.name = params.name?.trim();
    t.filter.eventType = params.eventType;
    t.filter.dateFrom = params.dateFrom;
    t.filter.dateTo = params.dateTo;
    t.filter.sort = params.sort;
    t.eventService.getHistoryPaged(t.filter).subscribe({
      next: (resp) => {
        this.listOfData = resp.data.items;
        this.total = resp.data.total;
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  async loadEventTypes() {
    const t = this;
    const resp = await t.eventService.getEventTypes();
    if (!resp?.data)
      return;
    t.eventTypes = new Map(Object.entries(resp.data).map(([key, value]) => [Number(key), value]));
    const eventColumn = t.listOfColumns.find(c => c.key === 'eventType');
    if (eventColumn) {
      eventColumn.listOfFilter = [...t.eventTypes.entries()].map(([k, v]) => ({
        text: v,
        value: k.toString()
      }));
    }
  }

  search(): void {
    const t = this;
    t.pageIndex = 1;
    t.loadTableData();
  }

  reset(key: string): void {
    const t = this;
    t.searchValue[key] = '';
    t.dateRange[key] = [];
    t.search();
  }

  onQueryParamsChange(params: NzTableQueryParams): void {
    const t = this;
    const { pageIndex, pageSize, sort, filter } = params;

    t.pageIndex = pageIndex;
    t.pageSize = pageSize;

    t.sortFields = sort
      .filter(s => s.value !== null)
      .map(s => ({ field: s.key!, order: s.value! }));

    for (const [_, values] of Object.entries(filter)) {
      if (values.key === 'eventType') {
        t.searchValue[values.key] = values.value?.[0] as string;
        t.pageIndex = 1;
      }
    }

    t.loadTableData();
  }

  getFiltersForRequest(): any {
    const t = this;
    const filters: any = {};

    if (t.searchValue['id']) {
      filters.id = t.searchValue['id'];
    }
    if (t.searchValue['text']) {
      filters.text = t.searchValue['text'];
    }
    if (t.searchValue['name']) {
      filters.name = t.searchValue['name'];
    }
    if (t.searchValue['eventType']) {
      filters.eventType = Number(t.searchValue['eventType']);
    }
    if (t.dateRange['date'] && t.dateRange['date'].length === 2) {
      filters.dateFrom = t.dateRange['date'][0].toISOString();
      filters.dateTo = t.dateRange['date'][1].toISOString();
    }

    return filters;
  }

  getSortForRequest(): any {
    if (this.sortFields.length === 0)
      return {};

    return {
      sort: this.sortFields.map(s => ({
        field: s.field,
        order: s.order
      }))
    };
  }

  getSortOrder(field: string): string | null {
    const sort = this.sortFields.find(s => s.field === field);
    return sort ? sort.order : null;
  }
}
