// -----------------------------------------------------------------------
//  <copyright file="CultureManager.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.AspNetCore.Localization
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.Extensions.Logging;
    using Pentagon.Extensions.Localization.Interfaces;

    /// <summary> Provides a ASP.NET Core middleware for population <see cref="ICultureContext" /> with current requested culture. </summary>
    public class CultureContextMiddleware
    {
        readonly RequestDelegate _next;

        public CultureContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync([NotNull] HttpContext context,
                                      [NotNull] ILogger<CultureContextMiddleware> logger,
                                      [NotNull] ICultureContextWriter cultureContext)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            if (cultureContext == null)
                throw new ArgumentNullException(nameof(cultureContext));

            var requestCulture = context.Features.Get<IRequestCultureFeature>();

            var ui     = requestCulture?.RequestCulture?.UICulture?.Name ?? CultureInfo.CurrentUICulture.Name;
            var format = requestCulture?.RequestCulture?.Culture?.Name ?? CultureInfo.CurrentCulture.Name;

            cultureContext.SetLanguage(uiCulture: ui, culture: format);

            await _next(context: context);

            logger.LogDebug($"Middleware ({nameof(CultureContextMiddleware)}) is processing response.");
        }
    }
}