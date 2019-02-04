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
using AzureFunctionsREST.Extensions;

namespace AzureFunctionsREST
{
    public static class PersonsService
    {
             
        [FunctionName("PersonsGet")]
        public static async Task<IActionResult> PersonsGet(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Persons")] HttpRequest httpRequest,
            ILogger logger
            )
        {
            await Task.CompletedTask;

            return new OkObjectResult(new List<Person>()
            {
                new Person(){ Id=Guid.Parse("88468cad14064f23b9d54d6940db3073"), FirstName="John", LastName="Smith", DateOfBirth=DateTime.Parse("1984-10-31"), Height=180},
                new Person(){ Id=Guid.Parse("d8cb37491b5e4048888a973dd95cf326"), FirstName="King", LastName="Robert", DateOfBirth=DateTime.Parse("1986-06-15"), Height=180}
            });
        }

        [FunctionName("PersonsGetById")]
        public static async Task<IActionResult> PersonsGetById(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Persons/{id}")] HttpRequest httpRequest,
            ILogger logger,
            String id
            )
        {
            await Task.CompletedTask;

            return new OkObjectResult(
                new Person() { Id = Guid.Parse("88468cad14064f23b9d54d6940db3073"), FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Parse("1984-10-31"), Height = 180 }
            );
        }

        //Model binding issue https://github.com/Azure/azure-functions-host/issues/3370
        [FunctionName("PersonsPost")]
        public static async Task<IActionResult> PersonsPost(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "Persons")] HttpRequest httpRequest,
            ILogger logger
            )
        {
            var validation = await httpRequest.ValidateAsync<Person>();
            if (validation.IsValid)
            {
                var person = validation.Model;
                //TODO: Model handling logic
            }
            return new OkResult();
        }

        [FunctionName("PersonsPatch")]
        public static async Task<IActionResult> PersonsPatch(
            [HttpTrigger(AuthorizationLevel.Function, "PATCH", "PUT", Route = "Persons")] HttpRequest httpRequest,
            ILogger logger
            )
        {
            var person = await httpRequest.BindModelAsync<Person>();
            //TODO: Model handling logic
            return new OkResult();
        }
    }
}
