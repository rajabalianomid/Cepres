using Microsoft.AspNetCore.StaticFiles;

namespace Cepres.Common
{
    public static class FileExtension
    {
        public static string GetContentType(this string filePath)
        {
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out string contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
