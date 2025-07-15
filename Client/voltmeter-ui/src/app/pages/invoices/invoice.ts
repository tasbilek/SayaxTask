import { Component, Input } from '@angular/core';
import { InvoiceModel } from '../../models/invoice.model';
import { CommonModule } from '@angular/common';

@Component({
  selector:"app-invoice-detail-modal",
  standalone:true,
  imports:[CommonModule],
  templateUrl: 'invoice.html'
})

export class Invoice {
  @Input() invoiceDetail?: InvoiceModel;
}
