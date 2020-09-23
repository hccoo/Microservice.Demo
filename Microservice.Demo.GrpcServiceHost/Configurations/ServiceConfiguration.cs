using Microservice.Demo.Service.Configurations;
using Microsoft.Extensions.Options;

namespace Microservice.Demo.ServiceHost.Configurations
{
    public class ServiceConfigurationAgent : IServiceConfigurationAgent
    {
        readonly IOptions<ConnectionStrings> _options;
        readonly IOptions<ApplicationSettings> _appSettingsOptions;
        public ServiceConfigurationAgent(IOptions<ConnectionStrings> options,
        IOptions<ApplicationSettings> appSettingsOptions)
        {
            _options = options;
            _appSettingsOptions = appSettingsOptions;
        }

        public string ConnectionString => _options.Value.ServiceDb;

        public string ApiHeaderKey => _appSettingsOptions.Value.AppHeaderKey;

        public string ApiHeaderValue => _appSettingsOptions.Value.AppHeaderValue;
    }

    public class ConnectionStrings
    {
        public string ServiceDb { get; set; }
    }

    public class ApplicationSettings
    {
        public string AppHeaderKey { get; set; }

        public string AppHeaderValue { get; set; }
        
    }
}
