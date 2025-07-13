using Commify.TaxCalculator.API.Models;
using Commify.TaxCalculator.API;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Commify.TaxCalculator.API.IntegrationTests.Tests;

[TestFixture]
public class TaxController_IntegrationTests
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!;

    [SetUp]
    public void Setup()
    {
        var dbName = Guid.NewGuid().ToString();
        Environment.SetEnvironmentVariable("INMEMORY_DB_NAME", dbName);

        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _client?.Dispose();
        _factory?.Dispose();
    }

    [Test]
    public async Task Calculate_ShouldReturnCorrectTaxResult_WhenGivenValidSalary()
    {
        var input = new SalaryInput { GrossSalary = 40000 };
        var response = await _client.PostAsJsonAsync("/api/tax/calculate", input);
        var result = await response.Content.ReadFromJsonAsync<TaxCalculationResult>();

        Assert.That(response.IsSuccessStatusCode, Is.True);
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.AnnualTaxPaid, Is.EqualTo(11000));
        Assert.That(result.NetAnnualSalary, Is.EqualTo(29000));
    }

    [Test]
    public async Task Calculate_ShouldReturnBadRequest_WhenSalaryIsZero()
    {
        var input = new SalaryInput { GrossSalary = 0 };
        var response = await _client.PostAsJsonAsync("/api/tax/calculate", input);
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Calculate_ShouldReturnBadRequest_WhenSalaryIsNegative()
    {
        var input = new SalaryInput { GrossSalary = -1000 };
        var response = await _client.PostAsJsonAsync("/api/tax/calculate", input);
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Calculate_ShouldHandleSalaryCrossingMultipleBands()
    {
        var input = new SalaryInput { GrossSalary = 25000 };
        var response = await _client.PostAsJsonAsync("/api/tax/calculate", input);
        var result = await response.Content.ReadFromJsonAsync<TaxCalculationResult>();

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.AnnualTaxPaid, Is.EqualTo(5000));
        Assert.That(result.NetAnnualSalary, Is.EqualTo(20000));
    }
}
