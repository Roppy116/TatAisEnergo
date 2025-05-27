import { Injectable } from '@angular/core';
import { IResponse } from '../../shared/interfaces';
import { HistoryModel } from '../../shared/models/history.model';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { HistoryFilterRequest } from '../../shared/models/filter/history.filter';

const apiUrl = 'history';

@Injectable({
    providedIn: 'root',
})

export class EventService {
    constructor(private api: ApiService) {}

    getHistoryPaged(filter: HistoryFilterRequest): Observable<IResponse<{ items: HistoryModel[], total: number }>> {
      return this.api.post<{ items: HistoryModel[], total: number }>(apiUrl, filter);
    }

    async getEventTypes(): Promise<IResponse<Record<number, string>>> {
      return (this.api.get<Record<number, string>>(apiUrl + '/event-type')).toPromise();
    }
}
