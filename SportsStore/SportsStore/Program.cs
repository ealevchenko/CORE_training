using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Регистрация службы хранилища
builder.Services.AddTransient<IProductRepository, FakeProductRepository>();

//builder.Configuration.AddIniFile("appsettings.ini");


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=List}/{id?}");

app.Run();
