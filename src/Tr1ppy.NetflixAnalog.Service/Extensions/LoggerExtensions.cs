using Microsoft.Extensions.Localization;

using NLog;

namespace Tr1ppy.NetflixAnalog.Service.Extensions;

public static class LoggerLocalizationExtensions
{
    public static void InfoLocalized<T>
    (
        this ILogger<T> logger,
        IStringLocalizer localizer,
        Enum key,
        params object[] args)
    {
        var message = localizer[key.ToString(), args];
        logger.LogInformation(message);
    }

    public static void InfoLocalized
    (
        this Logger logger, 
        IStringLocalizer localizer,
        Enum key,  
        params object[] args
    )
    {
        var message = localizer[key.ToString(), args];
        logger.Info(message);
    }

    public static void InfoLocalized
    (
        this Logger logger,
        IStringLocalizer localizer,
        string key,
        params object[] args
    )
    {
        var message = localizer[key, args];
        logger.Info(message);
    }

    public static void InfoLocalized
    (
        this Logger logger,
        IStringLocalizer localizer,
        Enum key
    )
    {
        var message = localizer[key.ToString()];
        logger.Info(message);
    }

    public static void ErrorLocalized
    (
        this Logger logger,
        IStringLocalizer localizer,
        Enum key,
        params object[] args
    )
    {
        var message = localizer.GetString(key.ToString(), args);
        logger.Error(message);
    }
    public static void ErrorLocalized
    (
        this Logger logger,
        IStringLocalizer localizer,
        Enum key,
        Exception exception,
        params object[] args
    )
    {
        var message = localizer.GetString(key.ToString(), args);
        logger.Error(exception, message);
    }

    public static void ErrorLocalized
    (
        this Logger logger,
        IStringLocalizer localizer,
        string key,
        params object[] args
    )
    {
        var message = localizer.GetString(key, args);
        logger.Error(message);
    }
}