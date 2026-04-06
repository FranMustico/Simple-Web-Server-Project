var builder = WebApplication.CreateBuilder(args);

/* THIS KESTREL CONFIGURATION STAYS DISABLED BECAUSE LTU's NETWORK SECURITY POLICY DOES NOT ALLOW
   A STATIC IP ADDRESS TO BE ASSIGNED.

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(System.Net.IPAddress.Parse("IP ADDRESS GOES HERE), 5000);
});

*/

var app = builder.Build();

// Log every request so the terminal output clearly shows the received HTTP request details.
app.Use(async (context, next) =>
{
    var timestamp = DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss zzz");
    Console.WriteLine($"[{timestamp}] {context.Request.Method} {context.Request.Path}{context.Request.QueryString} {context.Request.Protocol}");

    await next();
});

// Static file middleware is intentionally omitted so the assignment logic is handled explicitly in C# endpoints.
app.MapWebsiteEndpoints();

app.Run();
