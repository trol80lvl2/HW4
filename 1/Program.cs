using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace _1
{
    class Program
    {
        static List<int> SimpleNum(int start, int finish)
        {
            List<int> simpleNumbersList = new List<int>();
            for (int i = start; i < finish; i++)
            {
                if (isSimple(i))
                {
                    simpleNumbersList.Add(i);
                }
            }
            return simpleNumbersList;
        }
        static bool isSimple(int N)
        {
            if (N < 2)
                return false;
            for (int i = 2; i <= (int)(N / 2); i++)
            {
                if (N % i == 0)
                    return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            BordersModel borders;
            ResultModel result;
            TimeSpan span;
            try
            {
                DateTime timeStart = DateTime.UtcNow;

                string json = File.ReadAllText("settings.json");
                borders = JsonSerializer.Deserialize<BordersModel>(json);
                
                List<int> simpleNumbersArr = SimpleNum(borders.PrimesFrom, borders.PrimesTo);
                span = DateTime.UtcNow - timeStart;
                result = new ResultModel() { Duration = span.ToString(), Error = null, Primes = simpleNumbersArr, Success = true };

                File.WriteAllText("result.json",JsonSerializer.Serialize(result));
            }
            catch (JsonException)
            {
                result = new ResultModel() { Duration = "00:00:00", Error = "JSON parse error. Check the structure of JSON file", Primes = null, Success = false };
                File.WriteAllText("result.json", JsonSerializer.Serialize(result));
            }
            catch(FileNotFoundException)
            {
                result = new ResultModel() { Duration = "00:00:00", Error = "File not found", Success = false, Primes = null };
                File.WriteAllText("result.json", JsonSerializer.Serialize(result));
            }
            catch(Exception e)
            {
                result = new ResultModel() { Duration = "00:00:00", Error = e.Message, Success = false, Primes = null };
                File.WriteAllText("result.json", JsonSerializer.Serialize(result));
            }

        }
    }
}
