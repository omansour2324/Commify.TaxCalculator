import { Component } from '@angular/core';
import { TaxService } from './tax.service';
import { SalaryInput, TaxCalculationResult } from '../shared/models';

@Component({
  selector: 'app-tax-calculator',
  templateUrl: './tax-calculator.component.html',
  styleUrls: ['./tax-calculator.component.scss']
})
export class TaxCalculatorComponent {
  salary: number = 0;
  result?: TaxCalculationResult;
  error: string = '';

  constructor(private taxService: TaxService) { }

  calculate() {
    if (this.salary <= 0) {
      this.error = 'Please enter a valid salary.';
      this.result = undefined;
      return;
    }

    this.taxService.calculateTax({ grossSalary: this.salary }).subscribe({
      next: (res) => {
        this.result = res;
        this.error = '';
      },
      error: (err) => {
        this.error = 'Calculation failed. Please try again.';
        this.result = undefined;
      }
    });
  }
}
