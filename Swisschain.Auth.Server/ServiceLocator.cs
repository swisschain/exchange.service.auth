using System.Collections.Generic;
using DotNetCoreDecorators;
using MyDependencies;
using Serilog.Core;
using Swisschain.Auth.Domains;

namespace Swisschain.Auth.Server
{
    public static class ServiceLocator
    {
        public static SettingsModel Settings { get; set; }

        public static IAuthCredentialsRepository AuthCredentialsRepository { get; private set; }

        public static Logger Logger { get; private set; }

        public static void Init(IServiceResolver sr)
        {
            AuthCredentialsRepository = sr.GetService<IAuthCredentialsRepository>();
            Logger = sr.GetService<Logger>();
        }
    }
}