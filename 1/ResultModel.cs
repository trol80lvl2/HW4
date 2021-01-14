using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace _1
{
    public class ResultModel
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("error")]
        public string Error { get; set; }
        [JsonPropertyName("duration")]
        public string Duration { get; set; }
        [JsonPropertyName("primes")]
        public List<int> Primes { get; set; }
    }
}
