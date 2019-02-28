using TamTechSDK.Common.Services.ContentService;
using TamTechSDK.Common.Services.ContentService.Galleries;
using TamTechSDK.Common.Services.ContentService.Reviews;
using TamTechSDK.Web.Controllers.Admin;
using TamTechSDK.Web.Services.Telemetry;
using TamTechSDK.Common.Services.Upload;
using TamTechSDK.Common.Services.Upload.Settings;
using TamTechSDK.Web.Services.Configurations.Cultures;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TamTechSDK.Web.Models.Shared.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;

namespace tantum.Areas.Admin.Controllers
{
    public class GalleriesController : GalleryBaseController
    {
        private readonly IStringLocalizer<AdminBaseController> _localizer;
        public GalleriesController(ITelemetryClientService telemetryClientService,
            IGalleriesService galeriesService,
             IUploadManagerService uploadManager, IStringLocalizer<AdminBaseController> localizer)
            : base(telemetryClientService, galeriesService, uploadManager, localizer)
        {
            _localizer = localizer;
        }



        private void sectionList()
        {
            #region sectionList
            List<SelectListItem> sections = new List<SelectListItem>()
            {
                 new SelectListItem
                { Text = _localizer["section_projects"] , Value = "projects"  },
                  new SelectListItem
                { Text = _localizer["section_works"] , Value = "works"  },
            };
            ViewBag.sectionList = sections;
            #endregion
        }

        public override Task<IActionResult> Index(Filter model)
        {
            sectionList();
            return base.Index(model);
        }

        public override Task<IActionResult> Create()
        {
            sectionList();
            return base.Create();
        }

        public override Task<IActionResult> Edit(Guid id)
        {
            sectionList();
            return base.Edit(id);
        }
    }
}