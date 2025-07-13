namespace Commify.TaxCalculator.API.Models;
public class TaxBand
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal LowerLimit { get; set; }
    public decimal? UpperLimit { get; set; }
    public int Rate { get; set; } 
}