using AdvertApi.Models;
using AdvertApi.Repository.DTO;
using AdvertApi.Repository.Interfaces;
using AutoMapper;
using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace AdvertApi.Repository.Classes
{
    public class AdvertStorageRepository : IAdvertStorageRepository
    {
        private readonly IMapper mapper;

        public AdvertStorageRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<string> Add(AdvertModel model)
        {
            AdvertDTO advertDTO = mapper.Map<AdvertDTO>(model);
            advertDTO.ID = new Guid().ToString();
            advertDTO.CreationDateTime = DateTime.UtcNow;
            advertDTO.Status = AdvertStatus.Pending;
            using (var client = new AmazonDynamoDBClient())
            {
                using (var context = new DynamoDBContext(client))
                {
                   await context.SaveAsync(advertDTO);
                }
            }

            return advertDTO.ID;
        }

        public Task<bool> Confirm(ConfirmAdvertModel model)
        {
            throw new NotImplementedException();
        }
    }
}
