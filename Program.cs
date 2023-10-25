using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApp.Data;
using WebApp.Services;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Rejestracja serwisów CRUD
builder.Services.AddScoped(typeof(ICrudService<>), typeof(CrudService<>));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IFirstPageService, FirstPageService>();
builder.Services.AddScoped<ISliderItemService, SliderItemService>();

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

app.MapControllerRoute(name: "Product",
              pattern: "{categoryName}/{productName}/{productId}.html",
              defaults: new {  controller = "Product", action = "Detail" });

app.MapControllerRoute(name: "Products",
              pattern: "{categoryName}/{categoryId}.html",
              defaults: new { controller = "Product", action = "Index" });

app.MapControllerRoute(name: "group",
pattern: "{categoryName}.html",
defaults: new { controller = "Category", action = "Index" });

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller}/{action}/{id?}",
      defaults: new {  controller = "Home", action = "Index" }
    );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
