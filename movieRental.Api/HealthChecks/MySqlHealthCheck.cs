using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MySql.Data.MySqlClient;

namespace movieRental.Api.HealthChecks
{
    public class MySqlHealthCheck : IHealthCheck
    {
        private readonly string _connectionString;
        private readonly int _retryCount;
        private readonly int _delayOnFailure;

        public MySqlHealthCheck(string connectionString, int retryCount, int delayOnFailure)
        {
            _connectionString = connectionString;
            _retryCount = retryCount;
            _delayOnFailure = delayOnFailure;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            for (int connectionAttempt = 0; connectionAttempt <= _retryCount; connectionAttempt++)
            {
                if (await TryConnect())
                {
                    return HealthCheckResult.Healthy();
                }

                if (connectionAttempt != _retryCount)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(_delayOnFailure));
                }
            }
            return HealthCheckResult.Unhealthy();
        }

        private async Task<bool> TryConnect()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
