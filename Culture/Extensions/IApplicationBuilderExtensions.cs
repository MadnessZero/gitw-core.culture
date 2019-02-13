using GitWCore.Culture;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Utilise le paramétrage de la culture dans l'application
        /// </summary>
        public static void UseCultureManager(this IApplicationBuilder app)
        {
            app.UseRequestLocalization(CultureManager.Current.Options);
        }
    }
}
