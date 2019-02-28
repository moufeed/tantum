
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using tantum.AppStartup.DirectBackendServices.ContentService.RequestPipeline;
using TamTechBackendSDK.ContentService.Repositories.CosmosRepositories;


namespace tantum.AppStartup.WebApplication.RequestPipeline
{
    public static class MigrationDbConfigurations
    {
        public static void MigrateDB(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.ContentService_MigrateDB(configuration);

        }
    }
}
