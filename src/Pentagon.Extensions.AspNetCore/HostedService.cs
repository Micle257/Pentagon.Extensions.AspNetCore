// -----------------------------------------------------------------------
//  <copyright file="HostedService.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.AspNetCore
{
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Microsoft.Extensions.Hosting;

    public abstract class HostedService : IHostedService
    {
        Task _executingTask;

        CancellationTokenSource _cts;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            _executingTask = ExecuteAsync(_cts.Token) ?? Task.CompletedTask;

            return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null || _cts == null)
                return;

            _cts.Cancel();

            await (Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken)) ?? Task.CompletedTask).ConfigureAwait(false);

            cancellationToken.ThrowIfCancellationRequested();
        }

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);
    }
}