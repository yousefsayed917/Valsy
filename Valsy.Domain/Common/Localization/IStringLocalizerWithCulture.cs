using Microsoft.Extensions.Localization;
using Valsy.Domain.Common.Enums;

namespace Valsy.Domain.Common.Localization
{
    public interface IStringLocalizerWithCulture
    {
        string GetCultureByLanguage(string key, Language language);
        IStringLocalizer GetInstance();
    }
}
