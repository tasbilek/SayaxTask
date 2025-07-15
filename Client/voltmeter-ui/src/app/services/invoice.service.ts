import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from './http.service';
import { ResultModel } from '../models/result.model';
import { InvoicesModel } from '../models/invoices.model';
import { InvoiceModel } from '../models/invoice.model';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class InvoiceService {
    constructor(private httpService: HttpService) { }

    headers = new HttpHeaders().set('Content-Type', 'application/json');

    getAllInvoices(): Observable<ResultModel<InvoicesModel[]>> {
        return this.httpService.get<ResultModel<InvoicesModel[]>>('invoices');
    }

    getInvoiceById(id: string): Observable<ResultModel<InvoiceModel>> {
        return this.httpService.get<ResultModel<InvoiceModel>>(`invoices/${id}`);
    }
}