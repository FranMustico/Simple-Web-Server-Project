var builder = WebApplication.CreateBuilder(args);

/* THIS SECTION OF THE CODE HAS BEEN DISABLED, AS LTU DOES NOT ALLOW FOR STATIC IP'S TO BE ASSIGNED

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(System.Net.IPAddress.Parse("IP ADDRESS GOES HERE), 5000);
});

*/

var app = builder.Build();

app.UseStaticFiles(); 
app.UseDefaultFiles(); 

app.MapWebsiteEndpoints(); 

app.Run();