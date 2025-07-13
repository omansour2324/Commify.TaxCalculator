namespace Commify.TaxCalculator.API.Repository;

using Commify.TaxCalculator.API.Models;
using Microsoft.EntityFrameworkCore;

public class TaxBandRepository : ITaxBandRepository
{
    private readonly ApplicationDbContext _db;
    public TaxBandRepository(ApplicationDbContext db) => _db = db;

    public async Task<IEnumerable<TaxBand>> GetAllAsync()
    {
        return await _db.TaxBands.OrderBy(b => b.LowerLimit).ToListAsync();
    }
}

