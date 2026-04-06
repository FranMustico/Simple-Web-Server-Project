using Microsoft.AspNetCore.StaticFiles;

internal sealed class WebsiteFileResolver
{
    private readonly string _webRootPath;
    private readonly string _webRootPathWithSeparator;
    private readonly FileExtensionContentTypeProvider _contentTypeProvider = new();

    public WebsiteFileResolver(IWebHostEnvironment environment)
    {
        _webRootPath = Path.GetFullPath(Path.Combine(environment.ContentRootPath, "wwwroot"));
        _webRootPathWithSeparator = _webRootPath.EndsWith(Path.DirectorySeparatorChar)
            ? _webRootPath
            : _webRootPath + Path.DirectorySeparatorChar;
    }

    public WebsiteFileResolution Resolve(params string[] pathSegments)
    {
        return Resolve(Path.Combine(pathSegments));
    }

    public WebsiteFileResolution Resolve(string relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
        {
            return WebsiteFileResolution.Invalid(relativePath ?? string.Empty);
        }

        // This cleans up the request path before I combine it with the local wwwroot folder.
        var sanitizedRelativePath = relativePath
            .Trim()
            .TrimStart('/', '\\')
            .Replace('/', Path.DirectorySeparatorChar)
            .Replace('\\', Path.DirectorySeparatorChar);

        var fullPath = Path.GetFullPath(Path.Combine(_webRootPath, sanitizedRelativePath));
        var isWithinWebRoot = fullPath.StartsWith(_webRootPathWithSeparator, StringComparison.OrdinalIgnoreCase)
            || string.Equals(fullPath, _webRootPath, StringComparison.OrdinalIgnoreCase);

        // If the resolved path escapes wwwroot, I treat it as invalid instead of serving it.
        if (!isWithinWebRoot)
        {
            return WebsiteFileResolution.Invalid(relativePath);
        }

        // A missing file should return a normal 404-style result instead of throwing an exception first.
        if (!File.Exists(fullPath))
        {
            return WebsiteFileResolution.Missing(relativePath, fullPath);
        }

        // This picks a content type from the file extension so the browser handles each file correctly.
        var contentType = _contentTypeProvider.TryGetContentType(fullPath, out var resolvedContentType)
            ? resolvedContentType
            : "application/octet-stream";

        return WebsiteFileResolution.Found(relativePath, fullPath, contentType);
    }
}

internal sealed record WebsiteFileResolution(
    WebsiteFileResolutionStatus Status,
    string RequestedPath,
    string? FullPath,
    string? ContentType)
{
    public bool IsFound => Status == WebsiteFileResolutionStatus.Found;

    public bool IsInvalidPath => Status == WebsiteFileResolutionStatus.InvalidPath;

    public static WebsiteFileResolution Found(string requestedPath, string fullPath, string contentType)
    {
        return new(WebsiteFileResolutionStatus.Found, requestedPath, fullPath, contentType);
    }

    public static WebsiteFileResolution Missing(string requestedPath, string fullPath)
    {
        return new(WebsiteFileResolutionStatus.Missing, requestedPath, fullPath, null);
    }

    public static WebsiteFileResolution Invalid(string requestedPath)
    {
        return new(WebsiteFileResolutionStatus.InvalidPath, requestedPath, null, null);
    }
}

internal enum WebsiteFileResolutionStatus
{
    Found,
    Missing,
    InvalidPath
}
