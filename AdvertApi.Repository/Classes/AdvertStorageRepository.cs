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
        private readonly IAmazonDynamoDB _amazonDynamoDB;

        public AdvertStorageRepository(IMapper mapper, IAmazonDynamoDB amazonDynamoDB)
        {
            this.mapper = mapper;
            _amazonDynamoDB = amazonDynamoDB;
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

        public async Task Confirm(ConfirmAdvertModel model)
        {

            using (var context = new DynamoDBContext(_amazonDynamoDB))
            {
                var record = await context.LoadAsync<AdvertDTO>(model.ID);
                if (record == null)
                {
                    throw new Exception("record not found");
                }
                if (record.Status == AdvertStatus.Success)
                {
                    await context.SaveAsync(record);
                }
                else
                {
                    await context.DeleteAsync(model.ID);
                }

            }
        }


        public async Task<bool> CheckHealthAsync()
        {

            using (var context = new DynamoDBContext(_amazonDynamoDB))
            {
                var tableData = await _amazonDynamoDB.DescribeTableAsync("Advert");

                return tableData.Table.TableStatus == TableStatus.ACTIVE;
            }

        }
    }
}
