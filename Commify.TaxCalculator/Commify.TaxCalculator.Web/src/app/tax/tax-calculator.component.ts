import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-tax-calculator',
  templateUrl: './tax-calculator.component.html',
  styleUrls: ['./tax-calculator.component.scss']
})
export class TaxCalculatorComponent {
  form: FormGroup;
  taxResult: any;
  errorMessage: string | null = null;

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.form = this.fb.group({
      grossSalary: ['', [Validators.required, Validators.min(0)]]
    });
  }

  calculateTax(): void {
    if (this.form.invalid) return;

    this.errorMessage = null;
    const grossSalary = this.form.value.grossSalary;

    this.http.post('/api/tax/calculate', { grossSalary }).subscribe({
      next: (result) => {
        this.taxResult = result;
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'An error occurred';
        this.taxResult = null;
      }
    });
  }
}
