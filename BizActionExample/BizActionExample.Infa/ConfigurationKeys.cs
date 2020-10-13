using BizActionExample.Infa;

[assembly: ConfigurationKey(nameof(EfCoreContext), ConfigurationKeys.ConnectionStringName)]

namespace BizActionExample.Infa
{
    internal class ConfigurationKeys
    {
        internal const string ConnectionStringName = "ConnectionStrings:EfCore";
    }
}