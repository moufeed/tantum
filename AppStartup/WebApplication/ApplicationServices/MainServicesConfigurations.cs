using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using tantum.AppStartup.DirectBackendServices.ContentService.ApplicationServices;
using tantum.AppStartup.DirectBackendServices.EmailService;
using tantum.AppStartup.DirectBackEndServices.FilesServices;
using tantum.AppStartup.MicroServices.ApplicationServices;
using tantum.Services.Configurations.AppSetting.Constants;

namespace tantum.AppStartup.WebApplication.ApplicationServices
{
    public static class MainServicesConfigurations
    {
        public static void AddMainServicesConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            bool useMicroServices = configuration.GetSection(AppSettings.WebApplicationSettings)
                                                 .GetSection(AppSettings.WebApplication_UseMicroServices).Get<bool>();
            if (useMicroServices)
            {
                services.MicroServices_AddUrlBackendSettingsServicesConfigurations(configuration);

                services.MicroServices_AddProvidersServicesConfigurations();
            }
            else
            {
                services.DirectBackendServices_ContentService_AddCommonHelpersServicesConfigurations(configuration);

                services.DirectBackendServices_ContentService_AddContentBLLServicesConfigurations();

                services.DirectBackendServices_ContentService_AddRepositoriesConfigurations(configuration);

                services.DirectBackendServices_ContentService_AddFluentValidationConfigurations();

                services.DirectBackendServices_AddFilesServicesConfigurationsConfigurations(configuration);

                services.DirectBackendServices_AddEmailServicesConfigurations(configuration);
            }
        }
    }
}
