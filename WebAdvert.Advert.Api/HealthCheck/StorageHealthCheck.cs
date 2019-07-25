using AdvertApi.Repository.Interfaces;
using Microsoft.Extensions.HealthChecks;
using System.Threading;
using System.Threading.Tasks;


namespace WebAdvert.Advert.Api.HealthCheck
{
    public class StorageHealthCheck : IHealthCheck
    {
        private readonly IAdvertStorageRepository _repository;

        public StorageHealthCheck(IAdvertStorageRepository repository)
        {
         _repository = repository;
        }
        public async ValueTask<IHealthCheckResult> CheckAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var isStorageOk = await _repository.CheckHealthAsync();
            return HealthCheckResult.FromStatus(isStorageOk ? CheckStatus.Healthy : CheckStatus.Unhealthy, "");
        }
    }
}
