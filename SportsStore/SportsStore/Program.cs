using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

IConfiguration Configuration = builder.Configuration;
var connectionString = Configuration["Data:SportStoreProducts:ConnectionString"];

builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddTransient<IProductRepository, EFProuctRepository>();
builder.Services.AddTransient<IOrderRepository, EFOrderRepository>();

// Регистрация службы SessionCart
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


// Включение поддержки сеансов
builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();
//app.AddMvc(options => options.EnableEndpointRouting = false);
IConfiguration configuration = app.Configuration;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();

}

app.UseStatusCodePages();

app.UseHttpsRedirection();

app.UseStaticFiles();

//app.UseDeveloperExceptionPage();
app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute(name: null, pattern: "{category}/Page{productPage:int}", defaults:new { Controller = "Product", action = "List"});
    endpoints.MapControllerRoute(name: null, pattern: "Page{productPage:int}", defaults:new { Controller = "Product", action = "List", productPage =1});   
    endpoints.MapControllerRoute(name: null, pattern: "{category}", defaults:new { Controller = "Product", action = "List", productPage =1});   
    endpoints.MapControllerRoute(name: null, pattern: "", defaults:new { Controller = "Product", action = "List", productPage =1});   
    endpoints.MapControllerRoute(name: null, pattern: "{controller=Product}/{action=List}/{id?}");
});

SeetData.EnsurePopulated(app);

app.Run();

