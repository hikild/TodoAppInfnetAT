using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository.Database;

namespace TodoApp
{
    public static class DeleteTask
    {
        [FunctionName("DeleteTask")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var id = new Guid(req.Query["id"]);

            var repository = new TodoAppDatabase();
            var result = repository.GetTaskById(id);
            if (result == null)
            {
                return new NotFoundResult();
            }
            await repository.DeleteTask(result);


            return new OkResult();
        }
    }
}
