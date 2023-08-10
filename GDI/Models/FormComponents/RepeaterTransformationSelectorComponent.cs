using CMS.CustomTables;
using CMS.DataEngine;
using GDI.Models.FormComponents;
using Kentico.Forms.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
[assembly: RegisterFormComponent(RepeaterTransformationSelectorComponent.IDENTIFIER, typeof(RepeaterTransformationSelectorComponent), "Drop-down for Transformation Selector ", IconClass = "icon-menu")]
namespace GDI.Models.FormComponents
{
    public class RepeaterTransformationSelectorComponent : FormComponent<RepeaterTransformationSelectorComponentProperties, string>
    {
        public const string IDENTIFIER = "TransformationSelectorComponent";

        [BindableProperty]
        public string SelectedValue { get; set; }

        // Retrieves data to be displayed in the selector
        public IEnumerable<SelectListItem> GetIndexItems()
        {
            string customTableClassName = "GDI.RepeaterTransformationList";
            DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);
            if (customTable != null)
            {
                var customTableData = CustomTableItemProvider.GetItems(customTableClassName)
                    .ToList();

                foreach (var item in customTableData)
                {
                    var listItem = new SelectListItem()
                    {
                        Value = item.GetStringValue("TransformationCodeName", ""),
                        Text = item.GetStringValue("TransformationDisplayName", "")
                    };

                    yield return listItem;
                }
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
