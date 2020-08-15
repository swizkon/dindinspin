using System.Collections.Generic;
using MongoDB.Driver;

namespace DinnerSpinner.Api.Domain.Services
{
    using Configuration;
    using Models;

    public class SpinnerService
    {
        private readonly IMongoCollection<Spinner> _spinners;

        public SpinnerService(IDatabaseSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);

            _spinners = database.GetCollection<Spinner>(nameof(Spinner));
        }

        public List<Spinner> Get() =>
            _spinners.Find(spinner => true).ToList();

        public Spinner Get(string id) =>
            _spinners.Find<Spinner>(spinner => spinner.Id == id).FirstOrDefault();

        public Spinner Create(Spinner spinner)
        {
            _spinners.InsertOne(spinner);
            return spinner;
        }

        public void Update(string id, Spinner spinnerIn) =>
            _spinners.ReplaceOne(spinner => spinner.Id == id, spinnerIn);

        public void Remove(Spinner spinnerIn) =>
            _spinners.DeleteOne(spinner => spinner.Id == spinnerIn.Id);

        public void Remove(string id) =>
            _spinners.DeleteOne(spinner => spinner.Id == id);

        public Spinner AddDinner(string spinnerId, Dinner dinner)
        {
            var spinner = Get(spinnerId);

            spinner.Dinners.Add(dinner);

            Update(spinner.Id, spinner);

            return spinner;
        }
    }
}