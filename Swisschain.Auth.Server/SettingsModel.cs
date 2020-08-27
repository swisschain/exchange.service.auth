using MyYamlSettingsParser;

namespace Swisschain.Auth.Server
{

    public class SettingsModel
    {
        [YamlProperty("AuthServer.Db.AzureStorageConnString")]
        public string AzureStorageConnString { get; set; }  

        [YamlProperty("AuthServer.Db.PostgresConnString")]
        public string PostgresConnString { get; set; }  

        [YamlProperty("AuthServer.MyNoSqlReaderHostPort")]
        public string MyNoSqlReaderHostPort { get; set; }
        
        [YamlProperty("AuthServer.MyNoSqlWriterHostPort")]
        public string MyNoSqlWriterHostPort { get; set; }      
        
        [YamlProperty("AuthServer.SeqUrl")]
        public string SeqUrl { get; set; }
        
        [YamlProperty("AuthServer.MaxItemsInCache")]
        public int MaxItemsInCache { get; set; }
        
        [YamlProperty("AuthServer.ServiceBusWriterHostPort")]
        public string ServiceBusWriter { get; set; }
        
        [YamlProperty("AuthServer.InternalAccountPatterns")]
        public string InternalAccountPatterns { get; set; }
    }
}