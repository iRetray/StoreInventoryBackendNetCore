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
                        message = "El producto no es válido."
                    });
                }

                var newUUID = Guid.NewGuid();
                var response = new
                {
                    Id = newUUID,
                    product.Name,
                    product.Amount,
                    product.Description
                };

                return Results.Ok(new
                {
                    Message = "Product created successfully!",
                    StatusCode = 200,
                    data = response
                });
            });

            app.Run();
        }
    }
}
