using MonitoCalibratrice.Application;
using MonitoCalibratrice.Components;
using MonitoCalibratrice.Domain.Entities;
using MonitoCalibratrice.Infrastructure;
using MonitoCalibratrice.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddAutoMapper(typeof(RawProductViewModelProfile));

var app = builder.Build();

// Esegui il seed dei dati
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await SeedDataAsync(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

// Metodo di seed per popolare il database in-memory
async Task SeedDataAsync(ApplicationDbContext context)
{
    // Se ci sono già dati, non eseguire il seed
    if (context.RawProducts.Any())
        return;

    // Crea un RawProduct
    var rawProduct = new RawProduct
    {
        Id = Guid.NewGuid(),
        Code = "MAN",
        Name = "Mandarino",
        Description = "Mandarino di qualità"
    };

    // Crea una Variety associata al RawProduct
    var variety = new Variety
    {
        Id = Guid.NewGuid(),
        Code = "NAD",
        Name = "Nadorcot",
        Description = "Varietà Nadorcot per mandarini",
        RawProductId = rawProduct.Id,
        RawProduct = rawProduct
    };

    // Crea un FinishedProduct
    var finishedProduct = new FinishedProduct
    {
        Id = Guid.NewGuid(),
        Code = "NAD34C12X1",
        Name = "Mandarini Nadorcott COOP cal. 3-4 12x1kg",
        Description = "Sacchetto da 1kg di mandarini"
    };

    // Crea un SecondaryPackaging
    var secondaryPackaging = new SecondaryPackaging
    {
        Id = Guid.NewGuid(),
        Code = "R1",
        Name = "CP6416"
    };

    var production = new ProductionBatch
    {
        Id = Guid.NewGuid(),
        Caliber = "3-4",
        RawProductId = rawProduct.Id,
        VarietyId = variety.Id,
        FinishedProductId = finishedProduct.Id,
        SecondaryPackagingId = secondaryPackaging.Id,
        StartTime = DateTime.Now
    };

    // Aggiungi le entità al contesto
    context.RawProducts.Add(rawProduct);
    context.Varieties.Add(variety);
    context.FinishedProducts.Add(finishedProduct);
    context.SecondaryPackagings.Add(secondaryPackaging);
    context.ProductionBatches.Add(production);

    await context.SaveChangesAsync();
}