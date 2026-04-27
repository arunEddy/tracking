var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews(); // IMPORTANT for MVC
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Map MVC Controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Country}/{action=Index}/{id?}");

// Razor Pages
app.MapRazorPages();

app.Run();