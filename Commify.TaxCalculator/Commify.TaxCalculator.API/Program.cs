
using Commify.TaxCalculator.API.Logic;
using Commify.TaxCalculator.API.Models;
using Commify.TaxCalculator.API.Repository;
using Commify.TaxCalculator.Server.Logic;
using Microsoft.EntityFrameworkCore;

namespace Commify.TaxCalculator.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // DB setup (using in-memory for now)
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                var dbName = Environment.GetEnvironmentVariable("INMEMORY_DB_NAME") ?? "TaxBandsDb";
                options.UseInMemoryDatabase(dbName);
            });

            // DI
            builder.Services.AddScoped<ITaxBandRepository, TaxBandRepository>();
            builder.Services.AddScoped<ITaxCalculatorService, TaxCalculatorService>();


            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            SeedData(app);

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
        public static void SeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.TaxBands.AddRange(
                new TaxBand { Name = "A", LowerLimit = 0, UpperLimit = 5000, Rate = 0 },
                new TaxBand { Name = "B", LowerLimit = 5000, UpperLimit = 20000, Rate = 20 },
                new TaxBand { Name = "C", LowerLimit = 20000, UpperLimit = null, Rate = 40 });
            db.SaveChanges();
        }
    }
}
