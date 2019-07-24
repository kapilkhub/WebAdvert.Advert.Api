using AdvertApi.Models;
using System.Threading.Tasks;

namespace AdvertApi.Repository.Interfaces
{
    public interface IAdvertStorageRepository
    {
        Task<string> Add(AdvertModel model);

        Task<bool> Confirm(ConfirmAdvertModel model);
    }
}
