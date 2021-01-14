using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace _1
{
    public class BordersModel
    {
        [JsonPropertyName("primesFrom")]
        public int PrimesFrom { get; set; }
        [JsonPropertyName("primesTo")]
        public int PrimesTo { get; set; }
    }
}
