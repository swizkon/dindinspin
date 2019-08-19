using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;

namespace DinDinSpinWeb.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        public SampleDataController(IConfiguration configuration, ILogger<SampleDataController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        private static string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IConfiguration _configuration;
        private readonly ILogger<SampleDataController> _logger;

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts(int startDateIndex)
        {
            _logger.LogInformation("Running the method WeatherForecasts");

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index + startDateIndex).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                Id = Guid.NewGuid().ToString()
            });
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<Spinner>> Spinners()
        {
            var containerName = "mycontainer";

            var account = CloudStorageAccount.Parse(_configuration["StorageConnectionString"]);
            var serviceClient = account.CreateCloudBlobClient();
            var container = serviceClient.GetContainerReference(containerName);

            await container.CreateIfNotExistsAsync();

            //await container.GetBlockBlobReference("file.json").SerializeObjectToBlobAsync(new Spinner());

            var spinner = await container.GetBlockBlobReference("file.json").DeserializeObjectFromBlobAsync<Spinner>();

            spinner = spinner ?? new Spinner();

            spinner.Summary = "A list of dinners";
            spinner.Id = spinner.Id ?? Guid.NewGuid().ToString();

            await container.GetBlockBlobReference("file.json").SerializeObjectToBlobAsync(spinner);

            return new List<Spinner>(new[] { spinner });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }
            public string Id { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }

        public class Spinner
        {
            public string Id { get; set; }

            public int TemperatureC { get; set; }

            public string Summary { get; set; }
        }
    }
}
