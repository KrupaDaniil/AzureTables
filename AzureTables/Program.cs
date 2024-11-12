using EntityModels.Services.Implementations;
using EntityModels.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

string? patch = builder.Configuration.GetConnectionString("AzureStorage");

if (patch != null)
{
    builder.Services.AddTransient<ITableStorageService, TableStorageService>(_=> new TableStorageService(patch));
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Azure Storage connection string not found.");
    Console.ResetColor();
}

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();