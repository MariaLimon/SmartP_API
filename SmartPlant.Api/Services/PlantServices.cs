using MongoDB.Driver;
using SmartPlant.Api.Models;
using SmartPlant.Api.Configurations;
using Microsoft.Extensions.Options;
using ZstdSharp.Unsafe;
using MongoDB.Bson;

using MongoDB.Bson.Serialization;
using System.Diagnostics;

namespace SmartPlant.Api.Services
{
    public class PlantServices
    {
        public readonly IMongoCollection<Plant> _PlantCollection;

        public PlantServices(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDB = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _PlantCollection = mongoDB.GetCollection<Plant>("Plant");

            //_UserCollection = mongoDB.GetCollection<User>(databaseSettings.Value.CollectionName);
        }
       
      public async Task<List<Plant>> GetAsync()
        {
            var plants = await _PlantCollection.Find(_ => true).ToListAsync();

            // Convertir ObjectId a cadena en la lista de plantas
            plants.ForEach(plant =>Convert.ToString(plant.UserId));

            return plants;
        }
        public async Task InsertPlant(Plant plantInsert)
        {
            await _PlantCollection.InsertOneAsync(plantInsert);
        }

        public async Task DeletePlant(string plantId)
        {
            var filter = Builders<Plant>.Filter.Eq(s=>s.Id, plantId);
            await _PlantCollection.DeleteOneAsync(filter);
        }

        public async Task UpdatePlant(Plant dataToUpdate)
        {
            var filter = Builders<Plant>.Filter.Eq(s=>s.Id, dataToUpdate.Id);
            await _PlantCollection.ReplaceOneAsync(filter,dataToUpdate);
        }
    }
}