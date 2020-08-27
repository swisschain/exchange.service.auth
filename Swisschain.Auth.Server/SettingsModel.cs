using MyYamlSettingsParser;

namespace Swisschain.Auth.Server
{

    public class SettingsModel
    {

        [YamlProperty("AuthServer.Db.PostgresConnString")]
        public string PostgresConnString { get; set; }  

        [YamlProperty("AuthServer.SeqUrl")]
        public string SeqUrl { get; set; }

    }
}