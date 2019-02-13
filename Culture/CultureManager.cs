using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GitWCore.Culture
{
    /// <summary>
    /// Cette classe permet de mettre en place et de configurer un système de culture géré par l'URL de l'application. _
    /// </summary>
    public class CultureManager
    {
        internal static CultureManager Current { get; private set; }
        internal string DefaultCulture { get; }
        internal RequestLocalizationOptions Options { get; }

        private readonly List<CultureInfo> _cultures;

        private CultureManager(string defaultCulture, params string[] supportedCultures)
        {
            DefaultCulture = defaultCulture;
            _cultures = new List<CultureInfo> { new CultureInfo(DefaultCulture) };
            _cultures.AddRange(supportedCultures.Select(c => new CultureInfo(c)));

            Options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(DefaultCulture),
                SupportedCultures = _cultures,
                SupportedUICultures = _cultures,
            };
            Options.RequestCultureProviders.Insert(0, new UrlRequestCultureProvider());
        }

        /// <summary>
        /// Configure le CultureManager avec les éléments fournis
        /// </summary>
        /// <param name="services">Collection de services d'injection de dépendances</param>
        /// <param name="resourcesPath">Chemin vers le dossier de ressources</param>
        /// <param name="defaultCulture">Culture par défaut de l'application</param>
        /// <param name="supportedCultures">Liste non exhaustive des cultures supportées par l'application</param>
        public static void Prepare(IServiceCollection services, string resourcesPath, string defaultCulture, params string[] supportedCultures)
        {
            Current = new CultureManager(defaultCulture, supportedCultures);

            services.AddLocalization(options => options.ResourcesPath = resourcesPath)
                .AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
        }


        internal bool IsSupported(string cultureName)
        {
            return _cultures.Any(c => c.Name.Equals(cultureName));
        }
    }
}
