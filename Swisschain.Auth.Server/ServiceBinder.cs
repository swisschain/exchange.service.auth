using System.Text;
using MyDependencies;
using MyPostgreSQL;
using Serilog;
using Serilog.Core;
using Swisschain.Auth.Domains;
using Swisschain.Auth.Postgres;

namespace Swisschain.Auth.Server
{
    public static class ServiceBinder
    {
        private const string AppName = "AuthServer";

        public static void BindPostgresRepositories(this IServiceRegistrator sr, SettingsModel settingsModel, string encodingKey, string encodingVector)
        {
            sr.Register<IAuthCredentialsRepository>(
                new AuthenticationCredentialsPostgresRepository(new PostgresConnection(settingsModel.PostgresConnString), 
                    Encoding.UTF8.GetBytes(encodingKey), 
                    Encoding.UTF8.GetBytes(encodingVector)));
        }

        
        public static Logger BindSeqLogger(this IServiceRegistrator sr, SettingsModel settingModel)
        {
            var logger = new LoggerConfiguration()
                .Enrich.WithProperty("app name", AppName)
                .Enrich.WithHttpRequestId()
                .Enrich.WithHttpRequestUrl()
                .WriteTo.Seq(settingModel.SeqUrl)
                .CreateLogger();

            sr.Register(logger);

            return logger;
        }

    }
}