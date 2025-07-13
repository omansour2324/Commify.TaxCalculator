namespace Commify.TaxCalculator.API.Repository;

using Commify.TaxCalculator.API.Models;
public interface ITaxBandRepository
{
    Task<IEnumerable<TaxBand>> GetAllAsync();
}

