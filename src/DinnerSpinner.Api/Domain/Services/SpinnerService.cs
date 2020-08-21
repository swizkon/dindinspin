using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DinnerSpinner.Api.Domain.Contracts;
using MongoDB.Driver;

namespace DinnerSpinner.Api.Domain.Services
{
    using Configuration;
    using Models;

    public class SpinnerService
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Spinner> _spinners;
        private readonly IMongoCollection<User> _users;

        public SpinnerService(IDatabaseSettings settings)
        {
            _database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _spinners = _database.GetCollection<Spinner>(nameof(Spinner));
        }

        public List<Spinner> Get() => _spinners.Find(spinner => true).ToList();

        public Spinner Get(string id) => _spinners.Find(spinner => spinner.Id == id).FirstOrDefault();

        public async Task<Spinner> Create(CreateSpinner createSpinner)
        {
            using var session = await _database.Client.StartSessionAsync();

            try
            {
                var spinner = new Spinner
                {
                    Name = createSpinner.Name,
                    Version = 1,
                    Members = new List<UserRef>
                    {
                        new UserRef
                        {
                            Name = createSpinner.OwnerName,
                            Email = createSpinner.OwnerEmail
                        }
                    }
                };

                await _spinners.InsertOneAsync(session, spinner);

                return spinner;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Task UpdateAsync(string id, Spinner spinnerIn) => _spinners.ReplaceOneAsync(spinner => spinner.Id == id, spinnerIn);

        public void Remove(string id) =>
            _spinners.DeleteOne(spinner => spinner.Id == id);

        public async Task<Spinner> AddDinner(string spinnerId, Dinner dinner)
        {
            var spinner = Get(spinnerId);

            spinner.Dinners.Add(dinner);

            await UpdateAsync(spinner.Id, spinner);

            return spinner;
        }
    }
}
