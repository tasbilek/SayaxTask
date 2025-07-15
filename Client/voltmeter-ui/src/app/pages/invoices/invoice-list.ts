import { Component, OnInit } from '@angular/core';
import { InvoicesModel } from '../../models/invoices.model';
import { CommonModule } from '@angular/common';
import { InvoiceModel } from '../../models/invoice.model';
import { Invoice } from './invoice';
import { ChangeDetectorRef } from '@angular/core';
import { InvoiceService } from '../../services/invoice.service';
import { ToastrService } from 'ngx-toastr';
declare var bootstrap: any;

@Component({
  selector: 'app-invoice-list',
  standalone: true,
  imports: [CommonModule, Invoice],
  templateUrl: './invoice-list.html'
})

export class InvoiceList implements OnInit {
  invoices: InvoicesModel[] = [];
  selectedInvoice?: InvoiceModel = new InvoiceModel();

  constructor(
    private invoiceService: InvoiceService, private cdr: ChangeDetectorRef, private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.loadAllInvoices();
  }

  loadAllInvoices() {
    this.invoiceService.getAllInvoices()
      .subscribe({
        next: res => {
          this.invoices = res.data!;
          this.cdr.detectChanges();
        },
        error: res => {
          this.toastr.error(`${res.error.statusCode} - ${res.error.errorMessages[0]}`, 'Hata');
        }
      });
  }

  loadInvoiceDetail(id: string) {
    this.invoiceService.getInvoiceById(id)
      .subscribe({
        next: res => {
          this.selectedInvoice = res.data;
          
          const modalElement = document.getElementById('invoiceDetailModal');
          const modal = new bootstrap.Modal(modalElement);
          modal.show();
          this.cdr.detectChanges();
        },
        error: res => {
          this.toastr.error(`${res.error.statusCode} - ${res.errorerrorMessages[0]}`, 'Hata');
        }
      });
  }
}