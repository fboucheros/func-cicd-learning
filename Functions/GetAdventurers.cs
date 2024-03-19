using System.Net;
using Bogus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace func_cicd_learning
{
    public class GetAdventurers
    {
        private readonly ILogger _logger;

        public GetAdventurers(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetAdventurers>();
        }

        [Function("GetAdventurers")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            Randomizer.Seed = new Random(DateTime.Now.Millisecond);
            Faker<Adventurer> fSeller = new Faker<Adventurer>()
                .RuleFor(a => a.Name, f => f.Name.FullName())
                .RuleFor(a => a.Level, f => f.Random.Number(1, 20))
                .RuleFor(a => a.Class, f => f.PickRandom<AdventurerClass>())
                .RuleFor(a => a.HP, f => f.Random.Number(10, 100));

            var adventurers = fSeller.Generate(10);

            var response = req.CreateResponse(HttpStatusCode.OK);
            //response.Headers.Add("Content-Type", "application/json");
            await response.WriteAsJsonAsync(adventurers);
            
            return response;
        }
    }
}
