namespace StoreInventoryBackend
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            var url = $"http://0.0.0.0:{port}";
            var target = Environment.GetEnvironmentVariable("TARGET") ?? "World";


            var app = builder.Build();

            app.Urls.Add($"http://0.0.0.0:{port}");

            app.MapGet("/", () => new
            {
                Message = $"StoreInventory API working! (from: {target})",
                StatusCode = 200,
            });

            app.MapPost("/create", (Product product) =>
            {
                var isInvalidName = string.IsNullOrEmpty(product.Name);
                var isInvalidDescription = string.IsNullOrEmpty(product.Description);
                if (product == null || isInvalidName || isInvalidDescription)
                {
                    return Results.BadRequest(new
                    {
                        message = "The product is not valid."
                    });
                }

                var newUUID = Guid.NewGuid().ToString();
                var response = new Product
                {
                    Id = newUUID,
                    Name = product.Name,
                    Amount = product.Amount,
                    Price = product.Price,
                    Description =product.Description
                };

                return Results.Ok(new
                {
                    Message = "Product created successfully!",
                    StatusCode = 200,
                    data = response
                });
            });

            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Gamer Laptop MSI GamingX",
                    Amount = 80,
                    Price = 3_500_000,
                    Description = "A powerful laptop with dedicated graphics card and high-resolution screen."
                },
                new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "SONY K30 Wireless Headphones",
                    Amount = 150,
                    Price = 625_000,
                    Description = "Bluetooth headphones with active noise cancellation and long-lasting battery."
                },
                new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "iPhone 18 Pro Max 4K UHD",
                    Amount = 25,
                    Price = 1_850_000,
                    Description = "Latest generation phone with 5G connectivity, high-quality camera and modern design."
                }
            };

            app.MapGet("/products", () =>
            {
                return Results.Ok(new
                {
                    Message = "Products fetched successfully!",
                    StatusCode = 200,
                    data = products
                });
            });

            app.Run();
        }
    }
}
