using Microsoft.Extensions.Logging;
using Store.Data.Contexts;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreContextSeed 
    {
        public static async Task SeedAsync(StoreDbContext context,ILoggerFactory loggerFactory)
        {
            try
            {
                if (context.ProductBrands != null && !context.ProductBrands.Any())
                {
                    var brandsdata = File.ReadAllText("../Store.Repository/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsdata);

                    if (brands is not null)
                        await context.ProductBrands.AddRangeAsync(brands);
                }

                if (context.ProductTypes != null && !context.ProductTypes.Any())
                {
                    var typesdata = File.ReadAllText("../Store.Repository/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesdata);

                    if (types is not null)
                        await context.ProductTypes.AddRangeAsync(types);
                }
                if (context.Products != null && !context.Products.Any())
                {
                    var prodata = File.ReadAllText("../Store.Repository/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(prodata);

                    if (products is not null)
                        await context.Products.AddRangeAsync(products);


                }

                if (context.deliveryMethods is not null && !context.deliveryMethods.Any())
                {
                    var deliveryData = File.ReadAllText("../Store.Repository/SeedData/delivery.json");
                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                    if (deliveryMethods is not null)
                        await context.deliveryMethods.AddRangeAsync(deliveryMethods);
                }
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }

        }
    }
}
