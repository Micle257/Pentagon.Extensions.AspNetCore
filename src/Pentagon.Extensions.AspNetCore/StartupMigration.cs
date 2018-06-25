// -----------------------------------------------------------------------
//  <copyright file="StartupMigration.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.AspNetCore
{
    using System.Linq;
    using System.Threading.Tasks;

    public class StartupMigration
    {
        const string DefaultSeedName = "seed";
        const string DefaultMigrationName = "migration";

        bool _isSeedRequested,
             _isMigrationRequested;

        public string SeedName { get; set; } = DefaultSeedName;
        public string MigrationName { get; set; } = DefaultMigrationName;

        public bool IsRequested(string[] args)
        {
            if (args == null || args.Length == 0)
                return false;
            if (args.Contains(MigrationName))
                _isMigrationRequested = true;
            if (args.Contains(SeedName))
                _isSeedRequested = true;
            return _isMigrationRequested || _isSeedRequested;
        }

        public async Task RunAsync(IMigrationApp app)
        {
            app.OnStartup(null);
            if (_isMigrationRequested)
                await app.RunMigrationsAsync();
            if (_isSeedRequested)
                await app.EnsureSeedAsync();
        }
    }
}