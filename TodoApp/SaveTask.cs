using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository.Model;
using Repository.Database;

namespace TodoApp
{
    public static class SaveTask
    {
        [FunctionName("SaveTask")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            AddTask data = JsonConvert.DeserializeObject<AddTask>(requestBody);

            data.Id = Guid.NewGuid();

            var repository = new TodoAppDatabase();
            await repository.SaveTask(data);

            return new CreatedResult("", data);
        }
    }
}
