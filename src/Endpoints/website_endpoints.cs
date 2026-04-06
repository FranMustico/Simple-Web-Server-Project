public static class WebsiteEndpoints
{
    public static void MapWebsiteEndpoints(this WebApplication app)
    {
        app.MapGet("/", async () =>
        { 
            try {
                var filePath = Path.Combine(app.Environment.ContentRootPath, "wwwroot", "Landing", "Landing.html");
                var file = await File.ReadAllTextAsync(filePath);
                return Results.Content(file, "text/html");
            } catch (Exception ex) { 
                return Results.NotFound($"Landing directory not found: {ex}"); 
            }
        });

        app.MapGet("/file", () =>
        {
            try {
                var filePath = Path.Combine(app.Environment.ContentRootPath, "wwwroot", "Assets", "Francisco_Mustico_.pdf");

                if (!File.Exists(filePath))
                {
                    return Results.NotFound();
                }

                return Results.File(filePath, "application/pdf");
            } catch (Exception ex) { 
                return Results.NotFound($"Requested file not found: {ex}"); 
            }
        });

        app.MapGet("{filePath}", (string filePath) =>
        {
            var fullPath = Path.Combine(app.Environment.ContentRootPath, "wwwroot", filePath);

            if (!File.Exists(fullPath))
            {
                return Results.NotFound($"File not found: {filePath}");
            }

            return Results.File(fullPath);
        });
    }
}