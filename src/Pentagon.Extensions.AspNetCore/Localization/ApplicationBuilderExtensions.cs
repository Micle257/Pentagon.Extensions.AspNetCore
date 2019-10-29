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

    public static class ApplicationBuilderExtensions
    {
        [NotNull]
        public static IApplicationBuilder UseCultureContext([NotNull] this IApplicationBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.UseMiddleware<CultureContextMiddleware>();
        }
    }
}