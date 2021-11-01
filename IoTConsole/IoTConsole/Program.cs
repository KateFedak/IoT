using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Models;

namespace IoTConsole
{
    class Program
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

       private ManualResetEvent mre = new(false);

        static  void Main(string[] args)
        {
            for (int i = 0; i < 5000; i++)
            {
                HttpPost();
            }
        }

        static  void HttpPost()
        {
            try
            {
                using var client = new HttpClient();
                var responsePost =  client.PostAsJsonAsync("https://localhost:44332/weatherforecast", Get()).Result;
                Console.WriteLine(responsePost.IsSuccessStatusCode ? $"Success" : $"Error ");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static Weather Get()
        {
            var rng = new Random();
            return new Weather
            {
                Id = rng.Next(1, 1000),
                TemperatureC = rng.Next(-20, 55),
                Name = Summaries[rng.Next(Summaries.Length)]
            };
        }


    }
}
