using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Calculator
{
    public class MemoryHealthCheck : IHealthCheck
    {
        #region Members

        private readonly IOptionsMonitor<MemoryCheckOptions> _options;

        #endregion

        #region Properties

        public String Name => "memory_check";

        #endregion

        #region Constructors

        public MemoryHealthCheck(IOptionsMonitor<MemoryCheckOptions> options)
        {
            _options = options;
        }

        #endregion

        #region Methods

        #region Public

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            MemoryCheckOptions options = _options.Get(context.Registration.Name);

            // Include GC information in the reported diagnostics.
            Int64 allocated = GC.GetTotalMemory(false);
            Dictionary<String, Object> data = new()
            {
                { "AllocatedBytes", allocated },
                { "Gen0Collections", GC.CollectionCount(0) },
                { "Gen1Collections", GC.CollectionCount(1) },
                { "Gen2Collections", GC.CollectionCount(2) }
            };
            HealthStatus status = allocated < options.Threshold ? HealthStatus.Healthy : context.Registration.FailureStatus;

            return Task.FromResult(new HealthCheckResult(
                status,
                "Reports degraded status if allocated bytes " +
                $">= {options.Threshold} bytes.",
                null,
                data));
        }

        #endregion

        #endregion
    }
}