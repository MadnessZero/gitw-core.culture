using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GitWCore.Culture
{
    internal class UrlRequestCultureProvider : IRequestCultureProvider
    {
        public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var url = httpContext.Request.Path;

            var parts = url.Value
                .Split('/')
                .Where(p => !string.IsNullOrWhiteSpace(p)).ToList();

            if (!parts.Any())
                return Task.FromResult(new ProviderCultureResult(CultureManager.Current.DefaultCulture));

            var culture = parts[0];
            var hasCulture = Regex.IsMatch(culture, @"^[a-z]{2}(?:-[A-Z]{2})?$");

            if (hasCulture && CultureManager.Current.IsSupported(culture))
                return Task.FromResult(new ProviderCultureResult(culture));

            return Task.FromResult(new ProviderCultureResult(CultureManager.Current.DefaultCulture));
        }
    }
}
