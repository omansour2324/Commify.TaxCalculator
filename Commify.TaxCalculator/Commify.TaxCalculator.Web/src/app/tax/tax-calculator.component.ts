import { Component } from '@angular/core';
import { TaxService } from './tax.service';
import { SalaryInput, TaxCalculationResult } from '../shared/models';

@Component({
  selector: 'app-tax-calculator',
  templateUrl: './tax-calculator.component.html',
  styleUrls: ['./tax-calculator.component.scss']
})
export class TaxCalculatorComponent {
  grossSalary: number = 0;
  taxResult?: TaxCalculationResult;
  error: string = '';

  constructor(private taxService: TaxService) { }

  calculateTax() {
    if (this.grossSalary <= 0) {
      this.error = 'Please enter a valid salary.';
      this.taxResult = undefined;
      return;
    }

    this.taxService.calculateTax({ grossSalary: this.grossSalary }).subscribe({
      next: (res) => {
        this.taxResult = res;
        this.error = '';
      },
      error: (err) => {
        this.error = 'Calculation failed. Please try again.';
        this.taxResult = undefined;
      }
    });
  }
}
