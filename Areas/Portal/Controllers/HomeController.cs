using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TamTechBackendSDK.EmailService.Services;
using TamTechSDK.Common.Models.ContactUs;
using TamTechSDK.Common.Models.Pagination;
using TamTechSDK.Common.Models.Sort;
using TamTechSDK.Common.Services.ContentService.City;
using TamTechSDK.Common.Services.ContentService.Galleries;
using TamTechSDK.Common.Services.ContentService.Galleries.Models;
using TamTechSDK.Common.Services.ContentService.Menus;
using TamTechSDK.Common.Services.ContentService.Pages;
using TamTechSDK.Common.Services.ContentService.Posts;
using TamTechSDK.Common.Services.ContentService.Posts.Models;
using TamTechSDK.Common.Services.ContentService.Request;
using TamTechSDK.Common.Services.ContentService.Services;
using TamTechSDK.Common.Services.ContentService.Sliders;
using TamTechSDK.Common.Services.ContentService.Testimonials;
using TamTechSDK.Common.Services.Email;
using TamTechSDK.Common.Services.Email.Models;
using TamTechSDK.Common.Services.Upload;
using TamTechSDK.Web;
using TamTechSDK.Web.Controllers.Portal;
using TamTechSDK.Web.Services.Configurations.AppSetting;
using TamTechSDK.Web.Services.Telemetry;
using tantum.Custom;

namespace tantum.Areas.Portal.Controllers
{

    public class HomeController : HomeBaseController
    {
        private readonly IPageService _pageService;
        private readonly IWebApplicationSettingsService _appSettingsService;
        private readonly ISlidersServices _sliderService;
        private readonly IGalleriesService _GalleriesService;
        private readonly IPostsService _postservice;
        private readonly IServicesService _servicesservice;
        private readonly ITestimonialsServices _TestimonialsServices;
        private IStringLocalizer<PublicBaseController> _localizer;
        private readonly IEmailSenderService _emailSender;
        public HomeController(ITelemetryClientService telemetryClientService,
                               IPageService pageService,
                               IMenusServices menusServices,
                               IConfiguration configuration,
                               IEmailSenderService emailSender, IWebApplicationSettingsService appSettingsService, IServicesService servicesservice,
                               ISlidersServices sliderService,
                               IRequestService requestService,
                               ICityService cityService,
                               IGalleriesService GalleriesService,
                               IPostsService postservice,
                               ITestimonialsServices testimonialservices,
                               IStringLocalizer<PublicBaseController> localizer,
                               IUploadManagerService uploadManager,
                               ITestimonialsServices TestimonialsServices)
                               : base(telemetryClientService, pageService,
                                        menusServices, configuration, emailSender, appSettingsService,
                                        servicesservice, sliderService, requestService, cityService, testimonialservices, uploadManager, localizer)

        {


            _TestimonialsServices = TestimonialsServices;
            _pageService = pageService;
            _appSettingsService = appSettingsService;
            _GalleriesService = GalleriesService;
            _sliderService = sliderService;
            _postservice = postservice;
            _servicesservice = servicesservice;
            _localizer = localizer;
            _emailSender = emailSender;
        }

        #region uitilities
       

         
         
        #endregion

        [ActionName(Departments.Index)]
        public override async Task<IActionResult> Index()
        {
            try
            {

                var publicMainSlider = await _sliderService.GetPublicByKeyAsync(_appSettingsService.GetHomePageMainSliderKey());
                if (publicMainSlider.Success)
                {
                    ViewBag.publicMainSlider = publicMainSlider.Data;
                }

                //var testimonial = await _TestimonialsServices.GetPublicListAsync(true);
                //if (testimonial.Success)
                //{
                //    ViewBag.testimonials = testimonial.Data;
                //}

              
                 
                var aboutSummary = await _pageService.GetPublicPageSummaryByIdAsync(_appSettingsService.GetHomePageSummuryId());

                if (aboutSummary.Success)
                {
                    ViewBag.aboutSummary = aboutSummary.Data;
                }


                var MissionVisionSlider = await _sliderService.GetPublicByKeyAsync(_localizer["MissionVisionSliderKey"]);
                if (MissionVisionSlider.Success)
                {
                    ViewBag.MissionVisionSlider = MissionVisionSlider.Data;
                }

                //ViewBag.Posts = await _postservice.GetPublicListAsync(4);
                //ViewBag.galleries = await _GalleriesService.GetPublicListAsync(4);

                 

                //var services = await _servicesservice.GetPublicListAsync(8, true);

                //if (services.Success)
                //{
                //    ViewBag.services = services.Data;
                //}

                //var publicClientsSlider = await _sliderService.GetPublicByKeyAsync(_appSettingsService.GetHomePageCarouselSliderKey());

                //if (publicClientsSlider.Success)
                //{
                //    ViewBag.publicClientsSlider = publicClientsSlider.Data;
                //}


            }
            catch (Exception ex)
            {
                TrackException(ex);
            }



            return base.View();
        }

        public async Task<JsonResult> DepartmentsContactUs(ContactUs model, IFormCollection collection)
        {
            var _collection = collection;
            var _email = string.Empty;
            var department = _collection["department"];
            var emailSettings = _appSettingsService.GetEmailSettings();

            if (string.Equals(Departments.Construction , department ,StringComparison.OrdinalIgnoreCase))
            {
                _email = _localizer["Construction_contact_email"];
            } 
            else if (string.Equals(Departments.EventManagement, department, StringComparison.OrdinalIgnoreCase)) {
                _email = _localizer["EventManagement_contact_email"];
            }
            else if (string.Equals(Departments.GiftItems, department, StringComparison.OrdinalIgnoreCase)) {
                _email = _localizer["GiftItems_contact_email"];
            }
            else if (string.Equals(Departments.DesignAndAnimation, department, StringComparison.OrdinalIgnoreCase)) {
                _email = _localizer["DesignAndAnimation_contact_email"];
            }
            else if (string.Equals(Departments.SmartSystems, department, StringComparison.OrdinalIgnoreCase)) {
                _email = _localizer["SmartSystems_contact_email"];
            }
            else if (string.Equals(Departments.SoftwareSolutions, department, StringComparison.OrdinalIgnoreCase)) {
                _email = _localizer["SoftwareSolutions_contact_email"];
            }
            else if (string.Equals(Departments.Telecommunications, department, StringComparison.OrdinalIgnoreCase)) {
                _email = _localizer["Telecommunications_contact_email"];
            }
            else if (string.Equals(Departments.InteriorDesign, department, StringComparison.OrdinalIgnoreCase)) {
                _email = _localizer["InteriorDesign_contact_email"];
            }
            else
            {
                _email = emailSettings.From;
            }
            
            try
            {
                if (ModelState.IsValid)
                {
                   
                    bool result = await _emailSender.SendAsync(new EmailModel
                    {
                        Email = emailSettings.From,
                        Subject = $"Department : {Departments.Construction}",
                        HtmlMessage = $"<table  cellspacing=\"0\" width=\"100%\" style=\"border:1px solid #bbb; \"><tr><td style=\"padding:5px;border-bottom : 1px solid #bbb;border-right:1px solid #bbb;width:25%\"><strong>Message from </strong></td><td style=\"padding:5px;border-bottom : 1px solid #bbb;\">{model.Name}</td></tr><tr><td style=\"padding:5px;border-bottom : 1px solid #bbb;border-right:1px solid #bbb;width:25%\"><strong>Message from Email</strong></td><td style=\"padding:5px;border-bottom : 1px solid #bbb;\">{model.Email}</td></tr><tr>  <td style=\"padding:5px;border-bottom : 1px solid #bbb;border-right:1px solid #bbb;\"><strong>phone number</strong></td><td style=\"padding:5px;border-bottom : 1px solid #bbb;\">{model.Mobile}</td></tr><tr>  <td style=\"padding:5px;border-right:1px solid #bbb;\"> <strong>Message</strong></td><td >{model.Message} </td></tr></table>",
                        ReasonOfSend = "ContactUs",
                        CC = (emailSettings.CC != null && emailSettings.CC.Count > 0) ? emailSettings.CC : new List<string>()
                    });


                    return Json(new { success = result });
                }
                return Json(new { success = false });
            }
            catch (Exception exc)
            {
                TrackException(exc);

                return Json(new
                {
                    Success = false,
                    errors = new Dictionary<string, string> { { "Error", "Something wrong" } }
                });

            }

        }

        #region profile
        public async Task<JsonResult> profile(PaginationModel Pagination , string slug)
        {
            GalleryList galleries = new GalleryList();

            try
            {
                var result = await _GalleriesService.GetPublicListAsync(Pagination , slug);
                galleries.Data = result.Data;
                galleries.Pagination = result.Pagination;

                return Json(new { Success = result.Data != null , data = result.Data , pagination = result.Pagination });
            }
            catch (Exception ex)
            {
                TrackException(ex);
                return Json(new
                {
                    Success = false,
                    errors = new Dictionary<string, string> { { "Error", "Something wrong" } }
                });
            }
            
        }
        public async Task<JsonResult> project(string slug)
        {
            var lang = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            GalleryDto gallery = new GalleryDto();
            try
            {
                var result = await _GalleriesService.GetPublicBySlugAsync(slug, lang);
                if (result.Success)
                {
                    return Json(new { Success = result.Data != null, data = result.Data, });
                }
            }
            catch (Exception ex)
            {
                TrackException(ex);
                return Json(new
                {
                    Success = false,
                    errors = new Dictionary<string, string> { { "Error", "Something wrong" } }
                });
            }

            return Json(new { Success = false  });
        }


        #endregion
        #region Departments


        public async Task SetInfo(string ClientsSliderKey, string galleriesSection, Guid SummaryId, string servicesSection)
        {
            try
            {
                GalleryList galleries = new GalleryList();
                PaginationModel Pagination = new PaginationModel()
                {
                    PageNumber = "1",
                    PageSize = 6
                };
                SortingModel SortBy = new SortingModel()
                {
                    SortBy = "DisplayOrder"
                };

                var services = await _servicesservice.GetPublicListAsync(50, servicesSection, true);
                var aboutSummary = await _pageService.GetPublicPageSummaryByIdAsync(SummaryId);
                var ClientsSlider = await _sliderService.GetPublicByKeyAsync(ClientsSliderKey);
                var galleriesList = await _GalleriesService.GetPublicListAsync(Pagination, galleriesSection, SortBy);

                if (services.Success)
                {
                    ViewBag.Services = services.Data;
                }

                if (aboutSummary.Success)
                {
                    ViewBag.aboutSummary = aboutSummary.Data;
                }

                if (ClientsSlider.Success)
                {
                    ViewBag.ClientsSlider = ClientsSlider.Data;
                }
                galleries.Data = galleriesList.Data;
                galleries.Pagination = galleriesList.Pagination;
                ViewBag.GalleryList = galleries;
            }
            catch (Exception ex)
            {
                TrackException(ex);
            }
        }

         
        [ActionName(Departments.Construction)]
        public async  Task<IActionResult> Construction()
        {
            ViewBag.Department          = Departments.Construction;
            ViewBag.DepartmentFolder    = "construction";
            ViewData["title"]           = _localizer[Departments.Construction];
            ViewData["department"]      = _localizer[Departments.Construction];
            ViewData["small_title"]     = _localizer["we_make"];
            ViewData["main_title"]      = _localizer["great_projects"];
            ViewData["summary"]         = _localizer["construction_summary"];
            ViewData["btn_txt"]         = _localizer["view_projects"];
            ViewData["portfolio_text"]  = _localizer["construction_our_portfolio_text"];
            ViewData["services_text"]   = _localizer["construction_our_services_text"];
            ViewData["contact_us_text"] = _localizer["construction_contact_us_text"];
            ViewData["contact_email_a"] = _localizer["construction_contact_email_a"];
            ViewData["contact_email_b"] = _localizer["construction_contact_email_b"];
            ViewData["contact_phone_a"] = _localizer["construction_contact_phone_a"];
            ViewData["contact_phone_b"] = _localizer["construction_contact_phone_b"];
            ViewData["contact_address"] = _localizer["construction_contact_address"];

            var ClientsSliderKey        = _localizer["constructions_clients_slider_key"];
            var galleriesSection        = _localizer[Departments.Construction];
            var SummaryId               = new Guid(_localizer["Construction_aboutus_summary"]);
            var servicesSection         = _localizer[Departments.Construction];
            await SetInfo(ClientsSliderKey, galleriesSection, SummaryId, servicesSection);
             
            return View("Department");
        }
       
        [ActionName(Departments.EventManagement)]
        public async Task<IActionResult> EventManagement()
        {
            ViewBag.Department          = Departments.EventManagement;
            ViewBag.DepartmentFolder    = "events";
            ViewData["title"]           = _localizer[Departments.EventManagement];
            ViewData["department"]      = _localizer[Departments.EventManagement];
            ViewData["small_title"]     = _localizer["Event_we_make"];
            ViewData["main_title"]      = _localizer["Event_great_projects"];
            ViewData["summary"]         = _localizer["event_management_summary"];
            ViewData["btn_txt"]         = _localizer["view_projects"];
            ViewData["portfolio_text"]  = _localizer["Event_our_portfolio_text"];
            ViewData["services_text"]   = _localizer["Event_our_services_text"];
            ViewData["contact_us_text"] = _localizer["Event_contact_us_text"];
            ViewData["contact_email_a"] = _localizer["Event_contact_email_a"];
            ViewData["contact_email_b"] = _localizer["Event_contact_email_b"];
            ViewData["contact_phone_a"] = _localizer["Event_contact_phone_a"];
            ViewData["contact_phone_b"] = _localizer["Event_contact_phone_b"];
            ViewData["contact_address"] = _localizer["Event_contact_address"];

            var ClientsSliderKey        = _localizer["Event_clients_slider_key"];
            var galleriesSection        = _localizer[Departments.EventManagement];
            var SummaryId               = new Guid(_localizer["Event_aboutus_summary"]);
            var servicesSection         = _localizer[Departments.EventManagement];
            await SetInfo(ClientsSliderKey, galleriesSection, SummaryId, servicesSection);
            return View("Department");
        }
         
        [ActionName(Departments.DesignAndAnimation)]
        public async Task<IActionResult> DesignAndAnimation()
        {
            ViewBag.Department          = Departments.DesignAndAnimation;
            ViewBag.DepartmentFolder    = "design_and_animation";
            ViewData["title"]           = _localizer[Departments.DesignAndAnimation];
            ViewData["department"]      = _localizer[Departments.DesignAndAnimation];
            ViewData["small_title"]     = _localizer["Design_we_make"];
            ViewData["main_title"]      = _localizer["Design_great_projects"];
            ViewData["summary"]         = _localizer["Design_summary"];
            ViewData["btn_txt"]         = _localizer["view_projects"];
            ViewData["portfolio_text"]  = _localizer["Design_our_portfolio_text"];
            ViewData["services_text"]   = _localizer["Design_our_services_text"];
            ViewData["contact_us_text"] = _localizer["Design_contact_us_text"];
            ViewData["contact_email_a"] = _localizer["Design_contact_email_a"];
            ViewData["contact_email_b"] = _localizer["Design_contact_email_b"];
            ViewData["contact_phone_a"] = _localizer["Design_contact_phone_a"];
            ViewData["contact_phone_b"] = _localizer["Design_contact_phone_b"];
            ViewData["contact_address"] = _localizer["Design_contact_address"];

            var ClientsSliderKey        = _localizer["Design_clients_slider_key"];
            var galleriesSection        = _localizer[Departments.DesignAndAnimation];
            var SummaryId               = new Guid(_localizer["Design_aboutus_summary"]);
            var servicesSection         = _localizer[Departments.DesignAndAnimation];
            await SetInfo(ClientsSliderKey, galleriesSection, SummaryId, servicesSection);

            return View("Department");
        }

        [ActionName(Departments.SmartSystems)]
        public async Task<IActionResult> SmartSystems()
        {
            ViewBag.Department          = Departments.SmartSystems;
            ViewBag.DepartmentFolder    = "smart_systems";
            ViewData["title"]           = _localizer[Departments.SmartSystems];
            ViewData["department"]      = _localizer[Departments.SmartSystems];
            ViewData["small_title"]     = _localizer["smart_we_make"];
            ViewData["main_title"]      = _localizer["smart_great_projects"];
            ViewData["summary"]         = _localizer["smart_summary"];
            ViewData["btn_txt"]         = _localizer["view_projects"];
            ViewData["portfolio_text"]  = _localizer["smart_our_portfolio_text"];
            ViewData["services_text"]   = _localizer["smart_our_services_text"];
            ViewData["contact_us_text"] = _localizer["smart_contact_us_text"];
            ViewData["contact_email_a"] = _localizer["smart_contact_email_a"];
            ViewData["contact_email_b"] = _localizer["smart_contact_email_b"];
            ViewData["contact_phone_a"] = _localizer["smart_contact_phone_a"];
            ViewData["contact_phone_b"] = _localizer["smart_contact_phone_b"];
            ViewData["contact_address"] = _localizer["smart_contact_address"];

            var ClientsSliderKey        = _localizer["smart_clients_slider_key"];
            var galleriesSection        = _localizer[Departments.SmartSystems];
            var SummaryId               = new Guid(_localizer["smart_aboutus_summary"]);
            var servicesSection         = _localizer[Departments.SmartSystems];
            await SetInfo(ClientsSliderKey, galleriesSection, SummaryId, servicesSection);

            return View("Department");
        }
         
        [ActionName(Departments.GiftItems)]
        public async Task<IActionResult> GiftItems()
        {
            ViewBag.Department          = Departments.GiftItems;
            ViewBag.DepartmentFolder    = "gift_items";
            ViewData["title"]           = _localizer[Departments.GiftItems];
            ViewData["department"]      = _localizer[Departments.GiftItems];
            ViewData["small_title"]     = _localizer["GiftItems_we_make"];
            ViewData["main_title"]      = _localizer["GiftItems_great_projects"];
            ViewData["summary"]         = _localizer["GiftItems_summary"];
            ViewData["btn_txt"]         = _localizer["view_projects"];
            ViewData["portfolio_text"]  = _localizer["GiftItems_our_portfolio_text"];
            ViewData["services_text"]   = _localizer["GiftItems_our_services_text"];
            ViewData["contact_us_text"] = _localizer["GiftItems_contact_us_text"];
            ViewData["contact_email_a"] = _localizer["GiftItems_contact_email_a"];
            ViewData["contact_email_b"] = _localizer["GiftItems_contact_email_b"];
            ViewData["contact_phone_a"] = _localizer["GiftItems_contact_phone_a"];
            ViewData["contact_phone_b"] = _localizer["GiftItems_contact_phone_b"];
            ViewData["contact_address"] = _localizer["GiftItems_contact_address"];

            var ClientsSliderKey        = _localizer["GiftItems_clients_slider_key"];
            var galleriesSection        = _localizer[Departments.GiftItems];
            var SummaryId               = new Guid(_localizer["GiftItems_aboutus_summary"]);
            var servicesSection         = _localizer[Departments.GiftItems];
            await SetInfo(ClientsSliderKey, galleriesSection, SummaryId, servicesSection);

            return View("Department");
        }
         
        [ActionName(Departments.SoftwareSolutions)]
        public async Task<IActionResult> SoftwareSolutions()
        {
            ViewBag.Department          = Departments.SoftwareSolutions;
            ViewBag.DepartmentFolder    = "software_solutions";
            ViewData["title"]           = _localizer[Departments.SoftwareSolutions];
            ViewData["department"]      = _localizer[Departments.SoftwareSolutions];
            ViewData["small_title"]     = _localizer["SoftwareSolution_we_make"];
            ViewData["main_title"]      = _localizer["SoftwareSolution_great_projects"];
            ViewData["summary"]         = _localizer["SoftwareSolution_summary"];
            ViewData["btn_txt"]         = _localizer["view_projects"];
            ViewData["portfolio_text"]  = _localizer["SoftwareSolution_our_portfolio_text"];
            ViewData["services_text"]   = _localizer["SoftwareSolution_our_services_text"];
            ViewData["contact_us_text"] = _localizer["SoftwareSolution_contact_us_text"];
            ViewData["contact_email_a"] = _localizer["SoftwareSolution_contact_email_a"];
            ViewData["contact_email_b"] = _localizer["SoftwareSolution_contact_email_b"];
            ViewData["contact_phone_a"] = _localizer["SoftwareSolution_contact_phone_a"];
            ViewData["contact_phone_b"] = _localizer["SoftwareSolution_contact_phone_b"];
            ViewData["contact_address"] = _localizer["SoftwareSolution_contact_address"];

            var ClientsSliderKey        = _localizer["SoftwareSolutions_clients_slider_key"];
            var galleriesSection        = _localizer[Departments.SoftwareSolutions];
            var SummaryId               = new Guid(_localizer["SoftwareSolutions_aboutus_summary"]);
            var servicesSection         = _localizer[Departments.SoftwareSolutions];
            await SetInfo(ClientsSliderKey, galleriesSection, SummaryId, servicesSection);

            return View("Department");
        }

        [ActionName(Departments.Telecommunications)]
        public async Task<IActionResult> Telecommunications()
        {
            ViewBag.Department          = Departments.Telecommunications;
            ViewBag.DepartmentFolder    = "telecommunications";
            ViewData["title"]           = _localizer[Departments.Telecommunications];
            ViewData["department"]      = _localizer[Departments.Telecommunications];
            ViewData["small_title"]     = _localizer["Telecommunications_we_make"];
            ViewData["main_title"]      = _localizer["Telecommunications_great_projects"];
            ViewData["summary"]         = _localizer["Telecommunications_summary"];
            ViewData["btn_txt"]         = _localizer["view_projects"];
            ViewData["portfolio_text"]  = _localizer["Telecommunications_our_portfolio_text"];
            ViewData["services_text"]   = _localizer["Telecommunications_our_services_text"];
            ViewData["contact_us_text"] = _localizer["Telecommunications_contact_us_text"];
            ViewData["contact_email_a"] = _localizer["Telecommunications_contact_email_a"];
            ViewData["contact_email_b"] = _localizer["Telecommunications_contact_email_b"];
            ViewData["contact_phone_a"] = _localizer["Telecommunications_contact_phone_a"];
            ViewData["contact_phone_b"] = _localizer["Telecommunications_contact_phone_b"];
            ViewData["contact_address"] = _localizer["Telecommunications_contact_address"];


            var ClientsSliderKey        = _localizer["Telecommunications_clients_slider_key"];
            var galleriesSection        = _localizer[Departments.Telecommunications];
            var SummaryId               = new Guid(_localizer["Telecommunications_aboutus_summary"]);
            var servicesSection         = _localizer[Departments.Telecommunications];
            await SetInfo(ClientsSliderKey, galleriesSection, SummaryId, servicesSection);

            return View("Department");
        }
         
        [ActionName(Departments.InteriorDesign)]
        public async Task<IActionResult> InteriorDesign()
        {
            ViewBag.Department          = Departments.InteriorDesign;
            ViewBag.DepartmentFolder    = "interior_design";
            ViewData["title"]           = _localizer[Departments.InteriorDesign];
            ViewData["department"]      = _localizer[Departments.InteriorDesign];
            ViewData["small_title"]     = _localizer["InteriorDesign_we_make"];
            ViewData["main_title"]      = _localizer["InteriorDesign_great_projects"];
            ViewData["summary"]         = _localizer["InteriorDesign_summary"];
            ViewData["btn_txt"]         = _localizer["view_projects"];
            ViewData["portfolio_text"]  = _localizer["InteriorDesign_our_portfolio_text"];
            ViewData["services_text"]   = _localizer["InteriorDesign_our_services_text"];
            ViewData["contact_us_text"] = _localizer["InteriorDesign_contact_us_text"];
            ViewData["contact_email_a"] = _localizer["InteriorDesign_contact_email_a"];
            ViewData["contact_email_b"] = _localizer["InteriorDesign_contact_email_b"];
            ViewData["contact_phone_a"] = _localizer["InteriorDesign_contact_phone_a"];
            ViewData["contact_phone_b"] = _localizer["InteriorDesign_contact_phone_b"];
            ViewData["contact_address"] = _localizer["InteriorDesign_contact_address"];

            var ClientsSliderKey = _localizer["InteriorDesign_clients_slider_key"];
            var galleriesSection = _localizer[Departments.InteriorDesign];
            var SummaryId = new Guid(_localizer["InteriorDesign_aboutus_summary"]);
            var servicesSection = _localizer[Departments.InteriorDesign] ;
            await SetInfo(ClientsSliderKey, galleriesSection, SummaryId, servicesSection);

            return View("Department");
        }
         
        #endregion
    }


    
}