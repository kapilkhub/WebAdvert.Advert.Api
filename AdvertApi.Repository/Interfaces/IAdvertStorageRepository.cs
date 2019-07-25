using AdvertApi.Models;
using System.Threading.Tasks;

namespace AdvertApi.Repository.Interfaces
{
    public interface IAdvertStorageRepository
    {
        Task<string> Add(AdvertModel model);

        Task Confirm(ConfirmAdvertModel model);

        Task<bool> CheckHealthAsync();
    }
}
