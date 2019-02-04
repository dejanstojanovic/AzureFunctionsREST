using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionsREST.Extensions
{
    public static class HttpRequestExtensions
    {
        public static async Task<T> ReadModelAsync<T>(this HttpRequest httpRequest)
        {
            using (var reader = new StreamReader(httpRequest.Body))
            {
                return JsonConvert.DeserializeObject<T>(await reader.ReadToEndAsync());
            }
        }

        public static async Task<(bool IsValid, ICollection<ValidationResult> ValidationResult)> ValidateAsync<T>(this HttpRequest httpRequest)
        {
            var model = await httpRequest.ReadModelAsync<T>();
            if (model != null)
            {
                var results = new List<ValidationResult>();
                Validator.TryValidateObject(model, new ValidationContext(model), results);
                return (IsValid: false, ValidationResult: results);
            }
            throw new ArgumentException($"No model of type {typeof(T).FullName} found in the request payload");
        }
    }
}
