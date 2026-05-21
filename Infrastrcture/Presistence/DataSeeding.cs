using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Domain.Entities.OrderModule;
using Domain.Entities.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Text.Json;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext,
                             UserManager<ApplicationUser> _userManager,
                             RoleManager<IdentityRole> _roleManager) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                //1. Ensure that all migrations are applied and the table structure is ready.
                if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }
                //2. Check if the ProductBrand table is empty before seeding data.
                if (!_dbContext.ProductBrands.Any())
                {
                    //var ProductBrandsData = await File.ReadAllTextAsync(@"..\Infrastrcture\Presistence\Data\DataSeeding\brands.json");
                    var ProductBrandsData = File.OpenRead(@"..\Infrastrcture\Presistence\Data\DataSeeding\brands.json");
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandsData);
                    if (ProductBrands is not null && ProductBrands.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                    }
                }
                //3. Check if the ProductType table is empty before seeding data.
                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypesData = File.OpenRead(@"..\Infrastrcture\Presistence\Data\DataSeeding\Types.json");
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypesData);
                    if (ProductTypes is not null && ProductTypes.Any())
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);
                    }
                }
                //4. Check if the Product table is empty before seeding data.
                if (!_dbContext.Products.Any())
                {
                    var ProductsData = File.OpenRead(@"..\Infrastrcture\Presistence\Data\DataSeeding\Products.json");
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                    if (Products is not null && Products.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(Products);
                    }
                }
                if (!_dbContext.Set<DeliveryMethod>().Any())
                {
                    var DeliveryMethodData = File.OpenRead(@"..\Infrastrcture\Presistence\Data\DataSeeding\delivery.json");
                    var DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodData);
                    if (DeliveryMethods is not null && DeliveryMethods.Any())
                    {
                        await _dbContext.Set<DeliveryMethod>().AddRangeAsync(DeliveryMethods);
                    }
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed // To Do: Implement logging mechanism
            }
        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                    await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
                }
                if (!_userManager.Users.Any())
                {
                    var user01 = new ApplicationUser
                    {
                        UserName = "MohamedAly",
                        Email = "Mohamed@gmail.com",
                        DisplayName = "Mohamed Aly",
                        Address = new Address
                        {
                            FirstName = "Mohamed",
                            LastName = "Aly",
                            Street = "123 Main St",
                            City = "Cairo",
                            Country = "Egypt"
                        }
                    };
                    var user02 = new ApplicationUser
                    {
                        UserName = "SalmaMohamed",
                        Email = "Salma@gmail.com",
                        DisplayName = "Salma Mohamed",
                        Address = new Address
                        {
                            FirstName = "Salma",
                            LastName = "Mohamed",
                            Street = "456 Main St",
                            City = "Sohag",
                            Country = "Egypt"
                        }
                    };
                    await _userManager.CreateAsync(user01, "Pa$$w0rd");
                    await _userManager.CreateAsync(user02, "Pa$$w0rd");
                    await _userManager.AddToRoleAsync(user01, "Admin");
                    await _userManager.AddToRoleAsync(user02, "SuperAdmin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while seeding identity data.", ex);
            }
        }


    }
}
