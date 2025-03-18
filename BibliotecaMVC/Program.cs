using Microsoft.EntityFrameworkCore;
using BibliotecaMVC.Data; // Asegurarme de usar el namespace correcto

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddControllersWithViews();

// Registrar el DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configurar el pipeline de middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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