namespace Commify.TaxCalculator.API.Logic;
using Commify.TaxCalculator.API.Models;

public interface ITaxCalculatorService
{
    Task<TaxCalculationResult> CalculateTaxAsync(decimal grossSalary);
}


