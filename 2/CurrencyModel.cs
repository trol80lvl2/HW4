using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2
{
    public class CurrencyModel
    {
        [JsonProperty("r030")]
        public int R030 {get;set;}
        [JsonProperty("txt")]
        public string CurName { get; set; }
        [JsonProperty("rate")]
        public decimal CurRate { get; set; }
        [JsonProperty("cc")]
        public string CurCode { get; set; }
        [JsonProperty("exchangedate")]
        public string Date { get; set; }

    }
}
