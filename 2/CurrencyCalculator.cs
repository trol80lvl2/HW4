using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2
{
    public class CurrencyCalculator
    {
        public void CalculateCurrency(CurrencyModel[] currencies, string originCurrency, string desiredCurrency, decimal amount)
        {
            decimal originCurrencyRate = (from s in currencies
                                          where s.CurCode == originCurrency
                                          select s.CurRate).FirstOrDefault();
            decimal desiredCurrencyRate = (from s in currencies
                                           where s.CurCode == desiredCurrency
                                           select s.CurRate).FirstOrDefault();
            if (originCurrencyRate == 0 || desiredCurrencyRate == 0)
                Console.WriteLine($"Pair {originCurrency} - {desiredCurrency} doesn't exist");
            else
                Console.WriteLine($"Currency {originCurrency}/{desiredCurrency} = {Math.Round(originCurrencyRate / desiredCurrencyRate, 4)},\nfor {amount} {originCurrency} " +
                    $"you will get {amount * Math.Round(originCurrencyRate / desiredCurrencyRate, 4)} {desiredCurrency} actual on {(from s in currencies select s.Date).FirstOrDefault()}");
        }
    }
}
