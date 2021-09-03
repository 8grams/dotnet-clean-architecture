using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace WebApi.Application.Misc
{
    public class Utils
    {
        private readonly IHttpContextAccessor _accessor;

        public Utils(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public static bool IsDevelopment()
        {
            return string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "development", StringComparison.InvariantCultureIgnoreCase);
        }

        public static long DateToTimestamps(DateTime tm)
        {
            return (tm.Ticks - 621355968000000000) / 10000000 * 1000;
        }

        public static string GetFileExtension(string filename)
        {
            return Path.GetExtension(filename).Replace(".", "");
        }

        public static string GenerateFileCode(string prefix, string path)
        {
            var encryptor = SHA256Managed.Create();
            byte[] bytes = encryptor.ComputeHash(Encoding.ASCII.GetBytes(path));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {  
                builder.Append(bytes[i].ToString("x2"));
            }  
            var checkshum = builder.ToString().Substring(0, 6);
            return prefix + "_" + checkshum;
        }

        public static string GetUploadLocation(string prefix, string filename)
        {
            return "/Uploads/" + prefix + "/" + DateTime.Now.Ticks + "_" + filename;
        }

        public static string GetValidTopicName(string str)
        {
            return Regex.Replace(str, "[^A-Za-z0-9]", "").Trim().ToLower();
        }

        public string GetValidUrl(string path)
        {
            if (this.IsValidUrl(path)) return path;

            var request = _accessor.HttpContext.Request;
            var host = request.Host.ToUriComponent();
            var pathBase = request.PathBase.ToUriComponent();
            return $"{request.Scheme}://{host}{pathBase}{path}";
        }

        private bool IsValidUrl(string path)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(path, UriKind.Absolute, out uriResult)  
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;
        }
    }
}
