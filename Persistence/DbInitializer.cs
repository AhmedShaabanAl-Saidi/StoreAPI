using System.Text.Json;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;

        public DbInitializer(StoreDbContext context)
        {
            _context = context;
        }
        public void Initialize()
        {
            try
            {
                //if (_context.Database.GetPendingMigrations().Any())
                //    _context.Database.Migrate();

                if (!_context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText(@"..\Persistence\Data\Seeding\types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    if (types is not null && types.Any())
                    {
                        _context.ProductTypes.AddRange(types);
                        _context.SaveChanges();
                    }
                }

                if (!_context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText(@"..\Persistence\Data\Seeding\brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    if (brands is not null && brands.Any())
                    {
                        _context.ProductBrands.AddRange(brands);
                        _context.SaveChanges();
                    }
                }

                if (!_context.Products.Any())
                {
                    var productsData = File.ReadAllText(@"..\Persistence\Data\Seeding\products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    if (products is not null && products.Any())
                    {
                        _context.Products.AddRange(products);
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during database initialization.", ex);
            }
        }
    }
}