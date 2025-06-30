using Application.Dtos;
using Application.Dtos.Requests;
using Application.Interfaces.Pattern;
using Domain.Enums;
using infrastructure;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add infrastructure services and Options
builder.Services.AddOptions(builder.Configuration);
builder.Services.AddServices(builder.Configuration);



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
string str = "";
using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IAuthStrategyFactory>();
    var Strategy = factory.CreateAuthStrategy("Web");
    // Simulated request
    var loginRequest = new LoginRequest(null, "zebo@wmsk.com", null, null, "MKShajshs,2n", "127.0.0.1", null, "Mobile");
    try
    {
        var authResponse = await Strategy.LoginAsync(loginRequest);
        str = authResponse.AccessToken.Value;
        
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Login failed: {ex.Message}");
    }
}

app.MapGet("/", () => "Token: "+str);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
