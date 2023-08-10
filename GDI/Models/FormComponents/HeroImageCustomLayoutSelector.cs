using CMS.CustomTables;
using CMS.DataEngine;
using GDI.Models.FormComponents;
using Kentico.Forms.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
[assembly: RegisterFormComponent(HeroImageCustomLayoutSelector.IDENTIFIER, typeof(HeroImageCustomLayoutSelector), "Drop-down for transformation Type Selector ", IconClass = "icon-menu")]
namespace GDI.Models.FormComponents
{
    public class HeroImageCustomLayoutSelector : FormComponent<HeroImageCustomLayoutSelectorProperties, string>
    {
        public const string IDENTIFIER = "HeroImageCustomLayoutSelector";

        [BindableProperty]
        public string SelectedValue { get; set; }

        /// <summary>
        /// Get Hero Image Custom Layout list 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetIndexItems()
        {
            string customTableClassName = "GDI.HeroImageCustomLayout";
            DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);
            if (customTable != null)
            {
                // Gets all data records from the custom table whose 'ItemText' field value starts with 'New text'
                var customTableData = CustomTableItemProvider.GetItems(customTableClassName)
                    .ToList();

                foreach (var item in customTableData)
                {
                    var listItem = new SelectListItem()
                    {
                        Value = item.GetStringValue("values", ""),
                        Text = item.GetStringValue("options", "")
                    };

                    yield return listItem;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetValue()
        {
            return SelectedValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void SetValue(string value)
        {
            SelectedValue = value;
        }
    }
}
