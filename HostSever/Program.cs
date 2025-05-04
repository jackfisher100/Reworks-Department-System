// https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-8.0&tabs=visual-studio
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/route-handlers?view=aspnetcore-8.0

using HostSever;
using HostSever.Classes;
using HostSever.Model;





var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton(new DatabaseLink("Data Source=JACK-DELL-WINDO\\SQLEXPRESS;Initial Catalog=Bentley;Integrated Security=True;TrustServerCertificate=True;"));
var app = builder.Build();

app.MapControllers();

app.Logger.LogInformation("The application has started");



app.Run("http://localhost:80");

