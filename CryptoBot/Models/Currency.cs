using System.Text.Json.Serialization;

namespace CryptoBot.Models;

public class Currency
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("exchange_rate")]
    public float ExchangeRate { get; set; }
}