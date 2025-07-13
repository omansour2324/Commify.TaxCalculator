namespace Commify.TaxCalculator.API.Models;
public class TaxCalculationResult
{
    public decimal GrossAnnualSalary { get; set; }

    public decimal GrossMonthlySalary => Math.Round(GrossAnnualSalary / 12, 2);

    public decimal AnnualTaxPaid { get; set; }

    public decimal MonthlyTaxPaid => Math.Round(AnnualTaxPaid / 12, 2);

    public decimal NetAnnualSalary => Math.Round(GrossAnnualSalary - AnnualTaxPaid, 2);

    public decimal NetMonthlySalary => Math.Round(NetAnnualSalary / 12, 2);
}
