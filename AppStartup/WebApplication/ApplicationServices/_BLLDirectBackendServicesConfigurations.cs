using Microsoft.Extensions.DependencyInjection;
using TamTechBackendSDK.ContentService.Services.Sections;
using TamTechSDK.Common.Services.ContentService;
using TamTechSDK.Common.Services.ContentService.Brands;
using TamTechSDK.Common.Services.ContentService.Categories;
using TamTechSDK.Common.Services.ContentService.Galleries;
using TamTechSDK.Common.Services.ContentService.Menus;
using TamTechSDK.Common.Services.ContentService.Pages;
using TamTechSDK.Common.Services.ContentService.Posts;
using TamTechSDK.Common.Services.ContentService.Products;

using TamTechSDK.Common.Services.ContentService.Reviews;
using TamTechSDK.Common.Services.ContentService.Sections;
using TamTechSDK.Common.Services.ContentService.Services;
using TamTechSDK.Common.Services.ContentService.Sliders;
using TamTechSDK.Common.Services.ContentService.Testimonials;
using TamTechSDK.Common.Services.Email;
using TamTechSDK.Common.Services.Identity;
using TamTechSDK.Common.Services.Upload;

namespace tantum.AppStartup.WebApplication.ApplicationServices
{
    public static class BLLDirectBackendServicesConfigurations
    {
        public static void _AddBusinessLogicDirectBackendServicesConfigurations(this IServiceCollection services)
        {
            #region Contents Service 

            services.AddScoped<IMenusServices, tantum.DirectBackendServices.ContentServices.Menus.DirectMenusServices>();
            services.AddTransient<ICategoriesServices, tantum.DirectBackendServices.ContentServices.Categories.DirectCategoriesServices>();
            services.AddTransient<IProductsManagerService, tantum.DirectBackendServices.ContentServices.Products.DirectProductsManagerService>();
            // services.AddTransient<IProductsService, Web.DirectBackendServices.ContentServices.Products.DirectProductsService> ();
            services.AddTransient<IBrandsServices, tantum.DirectBackendServices.ContentServices.Brands.DirectBrandsServices>();
            services.AddTransient<IPageService, tantum.DirectBackendServices.ContentServices.Pages.DirectPagesService>();
            services.AddTransient<IReviewsServices, tantum.DirectBackendServices.ContentServices.Reviews.DirectReviewsServices>();
            services.AddTransient<IServicesService, tantum.DirectBackendServices.ContentServices.Services.DirectServicesService>();
            services.AddTransient<IContentServices, tantum.DirectBackendServices.ContentServices.Content.DirectContentService>();
            services.AddTransient<ISlidersServices, tantum.DirectBackendServices.ContentServices.Sliders.DirectSlidersService>();
            services.AddTransient<IGalleriesService, tantum.DirectBackendServices.ContentServices.Galleries.DirectGalleriesService>();
            services.AddTransient<IPostsService, tantum.DirectBackendServices.ContentServices.Posts.DirectPostsService>();
            services.AddTransient<ISectionsService, tantum.DirectBackendServices.ContentServices.Sections.DirectSectionsService>();
            services.AddTransient<TamTechSDK.Common.Services.ContentService.Request.IRequestService, tantum.DirectBackendServices.ContentServices.Request.DirectRequestService>();
            services.AddTransient<TamTechSDK.Common.Services.ContentService.Status.IStatusService, tantum.DirectBackendServices.ContentServices.Status.DirectSatusService>();
            services.AddTransient<TamTechSDK.Common.Services.ContentService.City.ICityService, tantum.DirectBackendServices.ContentServices.City.DirectCityService>();
            services.AddTransient<ITestimonialsServices, tantum.DirectBackendServices.ContentServices.Testimonials.DirectTestimonialsServices>();
            #endregion

            #region Upload Service 

            services.AddTransient<IUploadService, tantum.DirectBackendServices.UploadService.DirectFileStorageService>();

            #endregion

            #region Email Service 

            services.AddTransient<IEmailSenderService, tantum.DirectBackendServices.EmailServices.DirectEmailServices>();

            #endregion

            #region Identity Service 

            services.AddTransient<IAuthService, tantum.DirectBackendServices.IdentityService.DirectAuthService>();
            services.AddTransient<IUsersManagerService, tantum.DirectBackendServices.IdentityService.UsersMenager.DirectUserMenagerServices>();
            services.AddTransient<IRolesService, tantum.DirectBackendServices.IdentityService.RolesService.DirectRolesService>();


            #endregion


        }
    }
}
