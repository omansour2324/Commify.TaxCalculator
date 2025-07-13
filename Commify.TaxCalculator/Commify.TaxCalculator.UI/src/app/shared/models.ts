export interface SalaryInput {
  grossSalary: number;
}

export interface TaxCalculationResult {
  grossAnnualSalary: number;
  grossMonthlySalary: number;
  netAnnualSalary: number;
  netMonthlySalary: number;
  annualTaxPaid: number;
  monthlyTaxPaid: number;
}
