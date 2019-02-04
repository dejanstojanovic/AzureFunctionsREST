using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using AzureFunctionsREST.Models;

namespace AzureFunctionsREST
{
    public static class PersonsService
    {
        [FunctionName("PersonsGet")]
        public static async Task<ActionResult<IEnumerable<Person>>> PersonsGet(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Persons")] HttpRequest httpRequest,
            ILogger logger
            )
        {
            await Task.CompletedTask;

            return new OkObjectResult(new List<Person>()
            {
                new Person(),
                new Person(),
                new Person()
            });
        }

        [FunctionName("PersonsGetById")]
        public static async Task<ActionResult<Person>> PersonsGetById(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Persons/{id}")] HttpRequest httpRequest,
            ILogger logger,
            String id
            )
        {
            await Task.CompletedTask;

            return new OkObjectResult(
                new Person()
            );
        }

        [FunctionName("PersonsPost")]
        public static async Task<IActionResult> PersonsPost(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "Persons")] HttpRequest httpRequest,
            ILogger logger
            )
        {
            throw new NotImplementedException();
        }

        [FunctionName("PersonsPatch")]
        public static async Task<IActionResult> PersonsPatch(
            [HttpTrigger(AuthorizationLevel.Function, "PATCH","PUT", Route = "Persons")] HttpRequest httpRequest,
            ILogger logger
            )
        {
            throw new NotImplementedException();
        }
    }
}
