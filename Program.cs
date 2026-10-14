using HelloWorldApp;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

// Lista em memória (simula uma base de dados simples)
var products = new List<Product>
{
    new Product { Id = 1, Name = "bola", Price = 1200.50m },
    new Product { Id = 2, Name = "Smartphon", Price = 800.00m },
    new Product { Id = 3, Name = "Tablet", Price = 450.75m }
};

// Rota inicial
app.MapGet("/", context =>
{
    context.Response.Redirect("products.html");
    return Task.CompletedTask;
});

// Endpoint GET -> devolve lista de produtos
app.MapGet("/products", () => Results.Json(products));

// Endpoint POST -> adiciona novo produto
app.MapPost("/products", (Product newProduct) =>
{
    // Gera ID automático
    newProduct.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
    products.Add(newProduct);
    return Results.Json(newProduct);
});

app.Run();