public static class WebsiteEndpoints
{
    public static void MapWebsiteEndpoints(this WebApplication app)
    {
        var fileResolver = new WebsiteFileResolver(app.Environment);

        // This is the main page of the project, so I return the landing HTML file myself instead of using static middleware.
        app.MapGet("/", async () =>
        {
            var landingPage = fileResolver.Resolve("Landing", "landing.html");

            if (!landingPage.IsFound)
            {
                return Results.Text("Landing page was not found.", statusCode: 404);
            }

            try
            {
                var file = await File.ReadAllTextAsync(landingPage.FullPath!);
                return Results.Content(file, "text/html");
            }
            catch (Exception ex)
            {
                app.Logger.LogError(ex, "Unexpected error while serving the landing page.");
                return Results.Text("Internal Server Error", statusCode: 500);
            }
        });

        // This route serves the required PDF file from a fixed location in the Assets folder.
        app.MapGet("/file", () =>
        {
            var pdfFile = fileResolver.Resolve("Assets", "Francisco_Mustico_.pdf");

            if (!pdfFile.IsFound)
            {
                return Results.Text("Requested file was not found.", statusCode: 404);
            }

            try
            {
                return Results.File(pdfFile.FullPath!, "application/pdf");
            }
            catch (Exception ex)
            {
                app.Logger.LogError(ex, "Unexpected error while serving the project PDF.");
                return Results.Text("Internal Server Error", statusCode: 500);
            }
        });

        // This catch-all route lets me request files like CSS, JS, images, or other assets by path.
        // The resolver makes sure the final path stays inside wwwroot so outside files cannot be accessed.
        app.MapGet("/{**filePath}", (string filePath) =>
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return Results.Text("Requested file was not found.", statusCode: 404);
            }

            var requestedFile = fileResolver.Resolve(filePath);

            if (requestedFile.IsInvalidPath)
            {
                app.Logger.LogWarning("Rejected file request outside wwwroot: {RequestedPath}", filePath);
                return Results.Text("Requested file was not found.", statusCode: 404);
            }

            if (!requestedFile.IsFound)
            {
                return Results.Text("Requested file was not found.", statusCode: 404);
            }

            try
            {
                return Results.File(requestedFile.FullPath!, requestedFile.ContentType);
            }
            catch (Exception ex)
            {
                app.Logger.LogError(ex, "Unexpected error while serving {RequestedPath}.", filePath);
                return Results.Text("Internal Server Error", statusCode: 500);
            }
        });
    }
}
