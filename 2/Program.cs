using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace _2
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            CurrencyCalculator calculator = new CurrencyCalculator();
            Console.WriteLine("'Currency exchange' made by Roman Holub");
            while (true)
            {
                string originCurrency = "";
                string desiredCurrency = "";
                decimal amount = 0;
                do
                {
                    Console.Write("Enter origin currency->");
                    originCurrency = Console.ReadLine().Trim().ToUpper();
                }
                while (originCurrency.Length != 3);


                do
                {
                    Console.Write("Enter desired currency->");
                    desiredCurrency = Console.ReadLine().Trim().ToUpper();
                }
                while (desiredCurrency.Length != 3);

                do
                {
                    Console.Write("Enter the amount you have->");
                    amount = Convert.ToDecimal(Console.ReadLine().Trim());
                }
                while (amount <= 0);

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        
                        HttpResponseMessage response = await client.GetAsync("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json");
                        response.EnsureSuccessStatusCode();

                        var json = await response.Content.ReadAsStringAsync();
                        var currencies = JsonConvert.DeserializeObject<CurrencyModel[]>(json);
                        File.WriteAllText("cache.json", JsonConvert.SerializeObject(currencies));
                        calculator.CalculateCurrency(currencies, originCurrency, desiredCurrency, amount);
                    }
                }
                catch
                {
                    Console.WriteLine("An error occurred while trying to update data.");
                    if (!File.Exists("cache.json"))
                    {
                        Console.WriteLine("Something went wrong");
                        return;
                    }
                    else
                    {
                        string json = File.ReadAllText("cache.json");
                        var currencies = JsonConvert.DeserializeObject<CurrencyModel[]>(json);
                        calculator.CalculateCurrency(currencies, originCurrency, desiredCurrency, amount);
                    }
                }
            }
        }
    }
}
