import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MunicipalTaxesModel } from '../../models/municipal-taxes.model';
import { MunicipalService } from '../../services/municipal.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: "app-municipal-taxes",
  standalone: true,
  imports: [CommonModule],
  templateUrl: './municipal-tax-list.html'
})

export class MunicipalTaxList implements OnInit {
  municipalTaxes: MunicipalTaxesModel[] = [];

  constructor(
    private municipalService: MunicipalService, private cdr: ChangeDetectorRef, private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.loadAllMunicipalTaxes();
  }

  loadAllMunicipalTaxes() {
    this.municipalService.getAllMunicipalTaxes()
      .subscribe({
        next: res => {
          this.municipalTaxes = res.data!;
          this.cdr.detectChanges();
        },
        error: res => {
          this.toastr.error(`${res.error.statusCode} - ${res.error.errorMessages[0]}`, 'Hata');
        }
      });
  }
}