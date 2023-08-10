using CMS.CustomTables;
using CMS.DataEngine;
using CMS.SiteProvider;
using GDI.Models.FormComponents;
using Kentico.Forms.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
[assembly: RegisterFormComponent(RepeaterPageTypeSelectorComponent.IDENTIFIER, typeof(RepeaterPageTypeSelectorComponent), "Drop-down for Page Type Selector ", IconClass = "icon-menu")]

namespace GDI.Models.FormComponents
{
    public class RepeaterPageTypeSelectorComponent : FormComponent<RepeaterPageTypeSelectorComponentProperties, string>
    {
        public const string IDENTIFIER = "PageTypeSelectorComponent";

        [BindableProperty]
        public string SelectedValue { get; set; }

        // Retrieves data to be displayed in the selector
        public IEnumerable<SelectListItem> GetIndexItems()
        {
            var pageTypeList = new DataQuery("GDI.Generic.qpagetypelist")
               .Execute();

            foreach (var item in pageTypeList.Tables[0].AsEnumerable().Cast<DataRow>())
            {
                var listItem = new SelectListItem()
                {
                    Value = Convert.ToString(item["ClassName"]),
                    Text = Convert.ToString(item["ClassDisplayName"])
                };

                yield return listItem;
            }
        }

        public override string GetValue()
        {
            return SelectedValue;
        }

        public override void SetValue(string value)
        {
            SelectedValue = value;
        }
    }
}
