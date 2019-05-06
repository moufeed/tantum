# notes

* change route Configurations

    ```c#
    mvcRoutes.MapRoute("homeurl", "{culture}/{area}/{action}/{*slug}",
                                new
                                {
                                    culture = CultureConstants.DefaultCulture.Culture,
                                    area = routingSettings.DefaultArea,
                                    controller = "Home",
                                    action = Departments.Index
                                });
    ```

* add new actions for each of departments insdie home controller.
* replace section field inside create service with code below (Form.cshtml)

```c#
 @using tantum.Custom
    @{
        List<SelectListItem> selectList = Departments.DepartmentsList
            .Select(D => new SelectListItem { Text = Localizer[D].Value, Value = Localizer[D].Value }).ToList();
    }
    <select asp-items="selectList" asp-for="Section" class="form-control" id="test"></select>
```

* override sectionList in admin Galleries Controller

```c#
 private void sectionList()
        {
            #region sectionList
                List<SelectListItem> selectList = Departments.DepartmentsList
                    .Select(D => new SelectListItem { Text = _localizer[D].Value, Value = _localizer[D].Value }).ToList();
            ViewBag.sectionList = selectList;
            #endregion
        }
```
