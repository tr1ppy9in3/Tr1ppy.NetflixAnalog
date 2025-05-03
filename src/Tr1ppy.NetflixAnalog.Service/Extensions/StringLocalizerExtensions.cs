using Microsoft.Extensions.Localization;

namespace Tr1ppy.NetflixAnalog.Service.Extensions;

public static class StringLocalizerExtensions
{
    public static LocalizedString Localize(this IStringLocalizer localizer, Enum key)
    {
        return localizer[key.ToString()];
    }

    public static LocalizedString LocalizeAndFormat(this IStringLocalizer localizer, Enum key, params object[] args)
    {
        var keyName = key.ToString();
        var localized = localizer[keyName, args];

        return localized;
    }

    public static string LocalizeString(this IStringLocalizer localizer, Enum key)
    {
        return localizer[key.ToString()];
    }
}