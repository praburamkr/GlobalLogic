// <copyright file="Calc.cs" company="MGM Resorts International">
//   Copyright (c) 2019 MGM Resorts International. All rights reserved.
// </copyright>
// <author>Andrii Marchenko</author>
// <date>7/23/2019</date>
// <summary>
//   Azure function to calculate numbers.
// </summary>

namespace SumFunctionApp
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Azure functions to calculate numbers.
    /// </summary>
    public static class Calc
    {
        /// <summary>
        /// Add two numbers function.
        /// </summary>
        /// <param name="req">
        /// HTTP trigger request. Must contain following query parameters:
        /// - "a" - first double number;
        /// - "b" - second double number;
        /// </param>
        /// <param name="log">Logger instance.</param>
        /// <returns>Sum of two numbers</returns>
        [FunctionName("GetSum")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger 'sum' function processed a request.");

                string a = req.Query["a"];

                if (string.IsNullOrEmpty(a))
                {
                    throw new NullReferenceException("'a' query parameter is missing.");
                }

                if (!double.TryParse(a, out double num1))
                {
                    throw new InvalidCastException("'a' query parameter must be double.");
                }

                string b = req.Query["b"];

                if (string.IsNullOrEmpty(b))
                {
                    throw new NullReferenceException("'b' query parameter is missing.");
                }

                if (!double.TryParse(b, out double num2))
                {
                    throw new InvalidCastException("'b' query parameter must be double.");
                }

                double sum = num1 + num2;

                log.LogInformation($"'GetSum' function result: {sum}.");

                return new OkObjectResult(sum);
            }
            catch (Exception ex)
            {
                log.LogError($"Sum function failed. Details - {ex}");

                return new BadRequestObjectResult($"'Sum' function failed: {ex.Message}");
            }
        }
    }
}
