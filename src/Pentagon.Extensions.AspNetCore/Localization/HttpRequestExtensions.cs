// -----------------------------------------------------------------------
//  <copyright file="HttpRequestExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.AspNetCore.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Http;

    /// <summary> Provides extensions method for <see cref="HttpRequest" />. </summary>
    public static class HttpRequestExtensions
    {
        /// <summary> Gets the accept languages from request header. </summary>
        /// <param name="request"> The request. </param>
        /// <returns> An iteration of <see cref="string" />. </returns>
        /// <exception cref="System.ArgumentNullException"> request is null </exception>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static IEnumerable<string> GetUserLanguages([NotNull] this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return request.GetTypedHeaders()?
                          .AcceptLanguage?
                          .OrderByDescending(x => x?.Quality ?? 1)
                          .Select(x => x?.Value.Value)
                          .Where(a => a != null) ?? Array.Empty<string>();
        }
    }
}