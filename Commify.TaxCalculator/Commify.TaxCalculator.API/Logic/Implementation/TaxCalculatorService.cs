using Commify.TaxCalculator.API.Logic;
using Commify.TaxCalculator.API.Models;
using Commify.TaxCalculator.API.Repository;

namespace Commify.TaxCalculator.Server.Logic
{
    public class TaxCalculatorService : ITaxCalculatorService
    {
        private readonly ITaxBandRepository _repo;

        public TaxCalculatorService(ITaxBandRepository repo)
        {
            _repo = repo;
        }

        public async Task<TaxCalculationResult> CalculateTaxAsync(decimal grossSalary)
        {
            var bands = (await _repo.GetAllAsync()).ToList();
            decimal tax = 0;

            foreach (var band in bands)
            {
                if (grossSalary <= band.LowerLimit) continue;

                var upper = band.UpperLimit ?? grossSalary;
                var taxable = Math.Min(grossSalary, upper) - band.LowerLimit;
                tax += taxable * (band.Rate / 100m);

                if (band.UpperLimit == null || grossSalary <= upper)
                    break;
            }

            return new TaxCalculationResult
            {
                GrossAnnualSalary = grossSalary,
                AnnualTaxPaid = Math.Round(tax, 2)
            };
        }
    }
}
