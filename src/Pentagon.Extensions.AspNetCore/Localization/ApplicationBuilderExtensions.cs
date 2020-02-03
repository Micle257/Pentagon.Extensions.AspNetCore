// -----------------------------------------------------------------------
//  <copyright file="ApplicationBuilderExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.AspNetCore.Localization
{
    using System;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Pentagon.Extensions.Localization.Interfaces;

    public static class ApplicationBuilderExtensions
    {
        [NotNull]
        public static IApplicationBuilder UseCultureContext([NotNull] this IApplicationBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.UseMiddleware<CultureContextMiddleware>();
        }

        [NotNull]
        public static IApplicationBuilder UseLocalizationPreCache([NotNull] this IApplicationBuilder builder)
        {
            return builder.Use(async (context, next) =>
                               {
                                   var localizationCache = context.RequestServices.GetRequiredService<ILocalizationCache>();

                                   await localizationCache.GetAllAsync().ConfigureAwait(false);

                                   await next().ConfigureAwait(false);
                               });
        }
    }
}