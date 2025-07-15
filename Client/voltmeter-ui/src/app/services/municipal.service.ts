import { Injectable } from "@angular/core";
import { HttpService } from "./http.service";
import { HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { ResultModel } from "../models/result.model";
import { MunicipalTaxesModel } from "../models/municipal-taxes.model";

@Injectable({
    providedIn: 'root'
})
export class MunicipalService {
    constructor(private httpService: HttpService) { }

    getAllMunicipalTaxes(): Observable<ResultModel<MunicipalTaxesModel[]>> {
        return this.httpService.get<ResultModel<MunicipalTaxesModel[]>>(`municipal-taxes`);
    }
}