using Commify.TaxCalculator.API.Models;
using Commify.TaxCalculator.API.Repository;
using Commify.TaxCalculator.Server.Logic;
using Moq;

namespace Commify.TaxCalculator.API.UnitTests.Tests;

[TestFixture]
public class TaxCalculatorService_UnitTests
{
    private Mock<ITaxBandRepository> _repoMock = null!;
    private TaxCalculatorService _service = null!;

    [SetUp]
    public void SetUp()
    {
        _repoMock = new Mock<ITaxBandRepository>();
        _service = new TaxCalculatorService(_repoMock.Object);
    }

    [Test]
    public async Task CalculateTaxAsync_ShouldReturnCorrectTax_WhenSalaryIs10000()
    {
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<TaxBand>
        {
            new() { Name = "A", LowerLimit = 0, UpperLimit = 5000, Rate = 0 },
            new() { Name = "B", LowerLimit = 5000, UpperLimit = 20000, Rate = 20 },
            new() { Name = "C", LowerLimit = 20000, UpperLimit = null, Rate = 40 }
        });

        var result = await _service.CalculateTaxAsync(10000);

        Assert.That(result.AnnualTaxPaid, Is.EqualTo(1000));
        Assert.That(result.NetAnnualSalary, Is.EqualTo(9000));
    }

    [Test]
    public async Task CalculateTaxAsync_ShouldReturnCorrectTax_WhenSalaryIs40000()
    {
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<TaxBand>
        {
            new() { Name = "A", LowerLimit = 0, UpperLimit = 5000, Rate = 0 },
            new() { Name = "B", LowerLimit = 5000, UpperLimit = 20000, Rate = 20 },
            new() { Name = "C", LowerLimit = 20000, UpperLimit = null, Rate = 40 }
        });

        var result = await _service.CalculateTaxAsync(40000);

        Assert.That(result.AnnualTaxPaid, Is.EqualTo(11000));
        Assert.That(result.NetAnnualSalary, Is.EqualTo(29000));
    }

    [Test]
    public async Task CalculateTaxAsync_ShouldReturnZeroTax_WhenSalaryIsWithinBandA()
    {
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<TaxBand>
        {
            new() { Name = "A", LowerLimit = 0, UpperLimit = 5000, Rate = 0 },
            new() { Name = "B", LowerLimit = 5000, UpperLimit = 20000, Rate = 20 },
            new() { Name = "C", LowerLimit = 20000, UpperLimit = null, Rate = 40 }
        });

        var result = await _service.CalculateTaxAsync(4000);

        Assert.That(result.AnnualTaxPaid, Is.EqualTo(0));
        Assert.That(result.NetAnnualSalary, Is.EqualTo(4000));
    }

    [Test]
    public async Task CalculateTaxAsync_ShouldReturnZeroTax_WhenNoBandsExist()
    {
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<TaxBand>());
        var result = await _service.CalculateTaxAsync(50000);
        Assert.That(result.AnnualTaxPaid, Is.EqualTo(0));
    }

    [Test]
    public async Task CalculateTaxAsync_ShouldCalculateCorrectly_WhenSalaryIsExactlyOnBandLimit()
    {
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<TaxBand>
        {
            new() { Name = "A", LowerLimit = 0, UpperLimit = 5000, Rate = 0 },
            new() { Name = "B", LowerLimit = 5000, UpperLimit = 20000, Rate = 20 },
            new() { Name = "C", LowerLimit = 20000, UpperLimit = null, Rate = 40 }
        });

        var result = await _service.CalculateTaxAsync(5000);
        Assert.That(result.AnnualTaxPaid, Is.EqualTo(0));
    }

    [Test]
    public async Task CalculateTaxAsync_ShouldCalculateCorrectly_WhenSalaryIsSlightlyOverBand()
    {
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<TaxBand>
        {
            new() { Name = "A", LowerLimit = 0, UpperLimit = 5000, Rate = 0 },
            new() { Name = "B", LowerLimit = 5000, UpperLimit = 20000, Rate = 20 },
            new() { Name = "C", LowerLimit = 20000, UpperLimit = null, Rate = 40 }
        });

        var result = await _service.CalculateTaxAsync(5001);
        Assert.That(result.AnnualTaxPaid, Is.EqualTo(0.20));
    }
}


