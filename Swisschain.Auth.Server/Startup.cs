using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyDependencies;
using MySettingsReader;
using ProtoBuf.Grpc.Server;
using Swisschain.Auth.Server.Grpc;

namespace Swisschain.Auth.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private const string EncodingKey = "ENCODING_KEY";
        private const string EncodingInitVector = "ENCODING_INIT_VECTOR";

        private static readonly SettingsModel Settings = SettingsReader.GetSettings<SettingsModel>(".swisschain-auth");

        private IConfiguration Configuration { get; }
        private readonly MyIoc _ioc = new MyIoc();

        public void ConfigureServices(IServiceCollection services)
        {
            ServiceLocator.Settings = Settings;
            
            services.AddApplicationInsightsTelemetry(Configuration);
            var logger = _ioc.BindSeqLogger(Settings);
            services.AddControllers();
            services.AddCodeFirstGrpc(options => { options.Interceptors.Add<ErrorLoggerInterceptor>(logger); });

            var (key, initVector) = GetAuthKeys();
            
            _ioc.BindPostgresRepositories(Settings, key, initVector);
            ServiceLocator.Init(_ioc);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapGrpcService<AuthApi>();
                });
        }

        private static (string key, string initVector) GetAuthKeys()
        {
            var key = Environment.GetEnvironmentVariable(EncodingKey);
            if (string.IsNullOrEmpty(key))
                throw new Exception($"Env Variable {EncodingKey} is not found");

            var initVector = Environment.GetEnvironmentVariable(EncodingInitVector);
            if (string.IsNullOrEmpty(EncodingInitVector))
                throw new Exception($"Env Variable {EncodingInitVector} is not found");

            return (key, initVector);
        }
    }
}