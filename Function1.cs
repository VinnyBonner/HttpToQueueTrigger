using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.Storage;

namespace HttpToQueueTrigger
{
    public static class Function1
    {
        [FunctionName("HttpExample")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [Queue("outqueue"), StorageAccount("AzureWebJobsStorage")] ICollector<string> msg,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string directory = req.Query["directory"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            directory = directory ?? data?.directory;

            if (!string.IsNullOrEmpty(directory))
            {
                // Add a message to the output collection.
                msg.Add(string.Format(directory));
            }
            return directory != null
                ? (ActionResult)new OkObjectResult($"Message added to the queue with {directory}")
                : new BadRequestObjectResult("Please pass a directory on the query string or in the request body");
        }
    }
}
