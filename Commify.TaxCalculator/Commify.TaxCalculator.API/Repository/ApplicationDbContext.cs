namespace Commify.TaxCalculator.API.Repository;

using Commify.TaxCalculator.API.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<TaxBand> TaxBands => Set<TaxBand>();
}

