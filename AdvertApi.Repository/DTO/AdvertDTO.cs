using AdvertApi.Models;
using Amazon.DynamoDBv2.DataModel;
using System;

namespace AdvertApi.Repository.DTO
{
    [DynamoDBTable("Advert")]
    public class AdvertDTO
    {
        [DynamoDBHashKey]
        public string ID { get; set; }

        [DynamoDBProperty]
        public string Title { get; set; }

        [DynamoDBProperty]
        public string Description { get; set; }

        [DynamoDBProperty]
        public double Price { get; set; }

        [DynamoDBProperty]
        public DateTime CreationDateTime { get; set; }

        [DynamoDBProperty]
        public AdvertStatus Status { get; set; }

    }
}
