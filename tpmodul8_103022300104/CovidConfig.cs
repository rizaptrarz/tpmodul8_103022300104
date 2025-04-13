using System;
using System.IO;
using System.Text.Json;

namespace tpmodul8_103022300104
{
    public class CovidConfig
    {
        private const string ConfigPath = "covid_config.json";

        public string satuan_suhu { get; set; } = "celcius";
        public int batas_hari_deman { get; set; } = 14;
        public string pesan_ditolak { get; set; } = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
        public string pesan_diterima { get; set; } = "Anda dipersilahkan untuk masuk ke dalam gedung ini";

        public CovidConfig()
        {
            LoadConfig();
        }

        private class CovidConfigData
        {
            public string satuan_suhu { get; set; }
            public int batas_hari_deman { get; set; }
            public string pesan_ditolak { get; set; }
            public string pesan_diterima { get; set; }
        }

        public void LoadConfig()
        {
            if (File.Exists(ConfigPath))
            {
                string json = File.ReadAllText(ConfigPath);
                var data = JsonSerializer.Deserialize<CovidConfigData>(json);
                if (data != null)
                {
                    satuan_suhu = data.satuan_suhu;
                    batas_hari_deman = data.batas_hari_deman;
                    pesan_ditolak = data.pesan_ditolak;
                    pesan_diterima = data.pesan_diterima;
                }
            }
            else
            {
                SaveConfig();
            }
        }

        public void SaveConfig()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(this, options);
            File.WriteAllText(ConfigPath, json);
        }

        public void UbahSatuan()
        {
            satuan_suhu = satuan_suhu.ToLower() == "celcius" ? "fahrenheit" : "celcius";
            SaveConfig();
        }
    }
}
