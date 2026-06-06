using Microsoft.Extensions.Localization;
using System.Globalization;
using Valsy.Domain.Common.Enums;
using Valsy.Domain.Common.Localization;

namespace Valsy.Infrastructure.Common.Localization
{
    public class StringLocalizerWithCulture : IStringLocalizerWithCulture
    {
        private readonly IStringLocalizer _stringLocalizer;
        public StringLocalizerWithCulture(IStringLocalizer stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
        public IStringLocalizer GetInstance()
        {
            return _stringLocalizer;
        }
        public string GetCultureByLanguage(string key, Language language)
        {
            CultureInfo requestedCulture = new CultureInfo("Ar-EG");
            if (language == Language.En)
                requestedCulture = new CultureInfo("En-US");
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            CultureInfo.CurrentCulture = requestedCulture;
            CultureInfo.CurrentUICulture = requestedCulture;
            string value = _stringLocalizer[key].Value;
            CultureInfo.CurrentCulture = currentCulture;
            CultureInfo.CurrentUICulture = currentCulture;
            return value;
        }

    }

}
