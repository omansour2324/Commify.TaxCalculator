import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import axios from 'axios';

@Component({
  selector: 'app-tax-calculator',
  templateUrl: './tax-calculator.component.html',
  styleUrls: ['./tax-calculator.component.scss']
})
export class TaxCalculatorComponent {
  form: FormGroup;
  taxResult: any;
  errorMessage: string | null = null;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      grossSalary: ['', [Validators.required, Validators.min(0)]]
    });
  }

  async calculateTax(): Promise<void> {
    if (this.form.invalid) return;

    this.errorMessage = null;
    const grossSalary = this.form.value.grossSalary;

    try {
      const response = await axios.post('/api/tax/calculate', { grossSalary });
      this.taxResult = response.data;
    } catch (err: any) {
      this.errorMessage = err.response?.data?.message || 'An error occurred';
      this.taxResult = null;
    }
  }
}
