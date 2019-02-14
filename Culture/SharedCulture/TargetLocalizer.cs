using Microsoft.Extensions.Localization;
using System.Reflection;

namespace GitWCore.Culture.SharedCulture
{
    /// <summary>
    /// 
    /// </summary>
    public class TargetLocalizer<T>
    {
        private readonly IStringLocalizer _localizer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public LocalizedString this[string key]
        {
            get
            {
                return _localizer[key];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="factory"></param>
        public TargetLocalizer(IStringLocalizerFactory factory)
        {
            var type = typeof(T);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create(type.Name, assemblyName.Name);
        }
    }
}
