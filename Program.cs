using Microsoft.EntityFrameworkCore;
using ProductCatalogApi.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();

    if (!dbContext.Products.Any())
    {
        dbContext.Products.AddRange(
            new ProductCatalogApi.Models.Product
            {
                Name = "Wireless Mouse",
                Description = "A compact wireless mouse.",
                Price = 24.99m,
                Category = "Electronics",
                StockQuantity = 50
            },
            new ProductCatalogApi.Models.Product
            {
                Name = "Mechanical Keyboard",
                Description = "A full-size mechanical keyboard.",
                Price = 79.99m,
                Category = "Electronics",
                StockQuantity = 25
            },
            new ProductCatalogApi.Models.Product
            {
                Name = "Desk Lamp",
                Description = "An adjustable LED desk lamp.",
                Price = 34.50m,
                Category = "Home",
                StockQuantity = 18
            });

        dbContext.SaveChanges();
    }
}

app.MapControllers();

app.Run();
