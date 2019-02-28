using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamTechSDK.Common.Services.ContentService.Menus;
using TamTechSDK.Common.Services.ContentService.Menus.Enums;
using TamTechSDK.Common.Services.ContentService.Menus.Models;

namespace tantum.TagHelpers
{
    [HtmlTargetElement("Menu")]
    public class Navigation : TagHelper
    {
        private readonly IPublicMenusService _publicMenusService;

        private Dictionary<string, string> paths;

        public string ListClass { get; set; } = string.Empty;

        public Navigation(IPublicMenusService publicMenusService)
        {
            _publicMenusService = publicMenusService;

            var culture = CultureInfo.CurrentCulture.Name;
            paths = new Dictionary<string, string>
                   {
                      { MenuItemTypeEnum.product.ToString()  , $"/{culture}/portal/products/Details" },
                      { MenuItemTypeEnum.page.ToString()  , $"/{culture}/portal/pages/v" },
                      { MenuItemTypeEnum.staticpage.ToString()  , $"/{culture}/portal" },
                      { MenuItemTypeEnum.service.ToString()  , $"/{culture}/portal/services/v" },
                      { MenuItemTypeEnum.section.ToString()  , $"/{culture}/portal/posts/s" },
                      { MenuItemTypeEnum.gallery.ToString()  , $"/{culture}/portal/galleries/v" }
                   };
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            var menu = await _publicMenusService.GetNavbarMenuAsync();
            if (menu != null && menu.MenuItems != null)
            {
                output.Content.SetHtmlContent(Print_UL(menu.MenuItems));
            }
        }


        private string Print_UL(List<MenuItemDto> MenuItems)
        {

            StringBuilder stringBuilder = new StringBuilder($"<ul class=\"navbar-nav mr-auto {ListClass}\">");

            if (MenuItems?.Count > 0)
            {
                foreach (MenuItemDto Menu in MenuItems)
                {
                    if (Menu.ItemType == MenuItemTypeEnum.dropdown.ToString())
                    {
                        Print_Li_Dropdown(Menu, stringBuilder);
                    }
                    else
                    {
                        Print_Li(Menu, stringBuilder);
                    }
                }
            }

            stringBuilder.Append("</ul>");
            return stringBuilder.ToString();
        }


        private void Create_Nav_Item(MenuItemDto Menu, StringBuilder stringBuilder, string path = "")
        {
            string li = "<li class=\"nav-item\">";
            li += $"<a class=\"nav-link\" href=\"{ path }/{ Menu.Url }\">{Menu.Title}</a>";
            li += "</li>";
            stringBuilder.Append(li);
        }

        private void Print_Li(MenuItemDto Menu, StringBuilder stringBuilder)
        {

            #region service
            if (Menu.ItemType.Equals(MenuItemTypeEnum.service.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                Create_Nav_Item(Menu, stringBuilder, (string)paths[MenuItemTypeEnum.service.ToString()]);
            }
            #endregion

            #region static page
            if (Menu.ItemType.Equals(MenuItemTypeEnum.staticpage.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                Create_Nav_Item(Menu, stringBuilder, (string)paths[MenuItemTypeEnum.staticpage.ToString()]);
            }
            #endregion

            #region page
            if (Menu.ItemType.Equals(MenuItemTypeEnum.page.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                Create_Nav_Item(Menu, stringBuilder, (string)paths[MenuItemTypeEnum.page.ToString()]);
            }
            #endregion

            #region product
            if (Menu.ItemType.Equals(MenuItemTypeEnum.product.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                Create_Nav_Item(Menu, stringBuilder, (string)paths[MenuItemTypeEnum.product.ToString()]);
            }
            #endregion


            #region section
            if (Menu.ItemType.Equals(MenuItemTypeEnum.section.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                Create_Nav_Item(Menu, stringBuilder, (string)paths[MenuItemTypeEnum.section.ToString()]);
            }
            #endregion


            #region Url
            if (Menu.ItemType.Equals(MenuItemTypeEnum.url.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                var target = (Menu.UrlTarget?.Length > 0) ? Menu.UrlTarget : "_blank";
                var classs = (Menu.Url.StartsWith('#')) ? "scroll-link" : ""; 
                string li = "<li class=\"nav-item\">";
                li += $"<a class=\"nav-link {classs}\"  href=\"{Menu.Url}\" target=\"{target}\">{Menu.Title}</a>";
                li += "</li>";
                stringBuilder.Append(li);
            }
            #endregion

            #region gallery
            if (Menu.ItemType.Equals(MenuItemTypeEnum.gallery.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                Create_Nav_Item(Menu, stringBuilder, (string)paths[MenuItemTypeEnum.gallery.ToString()]);
            }
            #endregion

        }


        private void Print_Li_Dropdown(MenuItemDto menu, StringBuilder stringBuilder)
        {
            string li = "<li class=\"nav-item dropdown\">";
            li += $" <a class=\"nav-link dropdown-toggle\" href=\"#\" id=\"navbarDropdown\" role=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">{menu.Title}</a>";
            li += " <div class=\"dropdown-menu\" aria-labelledby=\"navbarDropdown\">";
            li += Create_DropdownItems(menu);
            li += "</div>";
            li += "</li>";
            stringBuilder.Append(li);
        }

        private string Create_DropdownItems(MenuItemDto menu)
        {
            string link = string.Empty;
            if (menu.MenuItems?.Count > 0)
            {
                foreach (MenuItemDto Menu in menu.MenuItems)
                {
                    link += Create_Dropdown_Link(Menu);
                }
            }
            return link;
        }

        private string Create_Dropdown_Link(MenuItemDto Menu)
        {
            string link = string.Empty;
            string path = string.Empty;

            if (Menu.ItemType.Equals(MenuItemTypeEnum.service.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                path = (string)paths[MenuItemTypeEnum.service.ToString()];
            }
            else if (Menu.ItemType.Equals(MenuItemTypeEnum.staticpage.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                path = (string)paths[MenuItemTypeEnum.staticpage.ToString()];
            }
            else if (Menu.ItemType.Equals(MenuItemTypeEnum.page.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                path = (string)paths[MenuItemTypeEnum.page.ToString()];
            }
            else if (Menu.ItemType.Equals(MenuItemTypeEnum.product.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                path = (string)paths[MenuItemTypeEnum.product.ToString()];
            }
            else if (Menu.ItemType.Equals(MenuItemTypeEnum.section.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                path = (string)paths[MenuItemTypeEnum.section.ToString()];
            }

            #region Url
            if (Menu.ItemType.Equals(MenuItemTypeEnum.url.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                var target = (Menu.UrlTarget?.Length > 0) ? Menu.UrlTarget : "_blank";
                link = $"<a class=\"dropdown-item\"  href=\"{Menu.Url}\" target=\"{target}\">{Menu.Title}</a>";
                return link;
            }
            #endregion

            link = $"<a class=\"dropdown-item\" href=\"{ path }/{ Menu.Url }\">{Menu.Title}</a>";

            return link;
        }
    }
}
