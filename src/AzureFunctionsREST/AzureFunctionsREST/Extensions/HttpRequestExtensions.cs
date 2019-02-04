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
        public static async Task<Byte[]> GetBodyAsync(this HttpRequest httpRequest)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await httpRequest.Body.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static async Task<String> GetBodyStringAsync(this HttpRequest httpRequest)
        {
            using (var reader = new StreamReader(httpRequest.Body))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public static async Task<T> BindModelAsync<T>(this HttpRequest httpRequest)
        {
            return JsonConvert.DeserializeObject<T>(await httpRequest.GetBodyStringAsync());
        }

        public static async Task<(T Model,bool IsValid, ICollection<ValidationResult> ValidationResult)> ValidateAsync<T>(this HttpRequest httpRequest)
        {
            var model = await httpRequest.BindModelAsync<T>();
            if (model != null)
            {
                var results = new List<ValidationResult>();
                Validator.TryValidateObject(model, new ValidationContext(model), results);
                return (Model: model, IsValid: false, ValidationResult: results);
            }
            throw new ArgumentException($"No model of type {typeof(T).FullName} found in the request payload");
        }
    }
}
