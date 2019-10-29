namespace Pentagon.Extensions.AspNetCore {
    using System;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.Extensions.DependencyInjection;
    using WebEssentials.AspNetCore.Pwa;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaults([NotNull] this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddHttpContextAccessor();

            services.AddRouting(options =>
                                {
                                    options.AppendTrailingSlash = false;
                                    options.LowercaseUrls       = true;
                                });

            services.AddResponseCompression(options =>
                                            {
                                                options.EnableForHttps = true;
                                                options.Providers.Add<GzipCompressionProvider>();
                                                options.Providers.Add<BrotliCompressionProvider>();
                                            });

            services.AddMvcCore();

            services.AddProgressiveWebApp(new PwaOptions
                                          {
                                                  OfflineRoute     = "/off",
                                                  RoutesToPreCache = "/, /chyba404"
                                          });

            return services;
        }

        public static IMvcBuilder AddPage([NotNull] this IMvcBuilder services, [NotNull] PageConvention convention)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (convention == null)
                throw new ArgumentNullException(nameof(convention));

            if (convention.Routes.Count == 0)
                return services;

            var routes = convention.Routes.ToArray();

            services.AddRazorPagesOptions(options =>
                                          {
                                              foreach (var route in routes)
                                              {
                                                  options.Conventions.AddPageRoute(convention.PageName, route);
                                              }
                                          });

            return services;
        }
    }
}