using AdvertApi.Repository.Classes;
using AdvertApi.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace WebAdvert.Advert.Api.Infrastructure.Advert
{
    public static class ConfigureAdvertServices
    {
        public static void ConfigureAdvert(this IServiceCollection service)
        {
            service.AddTransient<IAdvertStorageRepository, AdvertStorageRepository>();
        }
    }
}
