import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SalaryInput, TaxCalculationResult } from '../shared/models';

@Injectable({
  providedIn: 'root'
})
export class TaxService {
  private readonly baseUrl = '/api/tax';

  constructor(private http: HttpClient) { }

  calculateTax(input: SalaryInput): Observable<TaxCalculationResult> {
    return this.http.post<TaxCalculationResult>(`${this.baseUrl}/calculate`, input);
  }
}
