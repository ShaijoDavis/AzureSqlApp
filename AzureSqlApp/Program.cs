using AzureSqlApp.Services;

var builder = WebApplication.CreateBuilder(args);

var appConfigConnectionString = "Endpoint=https://azureappconfig500.azconfig.io;Id=BVv5;Secret=2JMy8AAgvKPd/qpoJvbaRPfytt9CY/wp4Ecc0g0V13o=";

builder.Host.ConfigureAppConfiguration(builder =>
{
    builder.AddAzureAppConfiguration(appConfigConnectionString);
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IProductService, ProductService>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
