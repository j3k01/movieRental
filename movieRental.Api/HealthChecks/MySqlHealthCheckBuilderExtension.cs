using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace movieRental.Api.HealthChecks
{
    public static class MySqlHealthCheckBuilderExtension
    {
        private const string NAME = "mysql";

        public static IHealthChecksBuilder AddMySql(this IHealthChecksBuilder builder,
            string connectionString,
            int retryCount,
            int delayOnFailure,
            string name = null,
            HealthStatus? failureStatus = null,
            IEnumerable<string> tags = null)
        {
            return builder.Add(new HealthCheckRegistration(
                name ?? NAME,
                sp => new MySqlHealthCheck(connectionString, retryCount, delayOnFailure),
                failureStatus,
                tags));
        }

    }
}
