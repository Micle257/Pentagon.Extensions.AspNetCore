// -----------------------------------------------------------------------
//  <copyright file="ApplicationBuilderExtensions.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.AspNetCore.Localization
{
    using Microsoft.AspNetCore.Builder;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCultureContext(this IApplicationBuilder builder) => builder.UseMiddleware<CultureContextMiddleware>();
    }
}