// -----------------------------------------------------------------------
//  <copyright file="IMigrationApp.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.AspNetCore
{
    using System.Threading.Tasks;

    public interface IMigrationApp
    {
        void OnStartup(string[] args);
        Task RunMigrationsAsync();
        Task EnsureSeedAsync();
    }
}