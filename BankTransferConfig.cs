using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Transfer
{
    public int threshold { get; set; }
    public int low_fee { get; set; }
    public int high_fee { get; set; }
}

public class Confirmation
{
    public string en { get; set; }
    public string id { get; set; }
}

public class BankTransferConfig
{
    public string lang { get; set; }
    public Transfer transfer { get; set; }
    public List<string> methods { get; set; }
    public Confirmation confirmation { get; set; }
    private const string configFilePath = "bank_transfer_config.json";
    private static BankTransferConfig defaultConfig = new BankTransferConfig
    {
        lang = "en",
        transfer = new Transfer
        {
            threshold = 25000000,
            low_fee = 6500,
            high_fee = 15000
        },
        methods = new List<string> { "RTO (real-time)", "SKN", "RTGS", "BI FAST" },
        confirmation = new Confirmation
        {
            en = "yes",
            id = "ya"
        }
    };
    public static BankTransferConfig LoadConfig()
    {
        if (!File.Exists(configFilePath))
        {
            SaveConfig(defaultConfig);
            return defaultConfig;
        }

        string json = File.ReadAllText(configFilePath);
        return JsonSerializer.Deserialize<BankTransferConfig>(json);
    }
    public static void SaveConfig(BankTransferConfig config)
    {
        string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(configFilePath, json);
    }
}
