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
    public static class UpdateTask
    {
        [FunctionName("UpdateTask")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            AddTask dataUpdate = JsonConvert.DeserializeObject<AddTask>(requestBody);

            var repository = new TodoAppDatabase();
            await repository.UpdateTask(dataUpdate);

             return new OkObjectResult(dataUpdate);
        }
    }
}
