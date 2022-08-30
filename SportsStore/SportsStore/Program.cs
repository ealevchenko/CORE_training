using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

IConfiguration Configuration = builder.Configuration;
var connectionString = Configuration["Data:SportStoreProducts:ConnectionString"];
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddTransient<IProductRepository, EFProuctRepository>();

//app.AddMvc(options => options.EnableEndpointRouting = false);


var app = builder.Build();


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

app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute(name: "pagination", pattern: "products/Page{productpage}", defaults:new { Controller = "Product", action = "List"});
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Product}/{action=List}/{id?}");
});

SeetData.EnsurePopulated(app);

app.Run();

