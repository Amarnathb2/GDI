    using CMS.Helpers;
using GDI.Models.FormComponents;
using Kentico.Forms.Web.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
[assembly: RegisterFormComponent(HeroImageButtonComponent.IDENTIFIER, typeof(HeroImageButtonComponent), "Drop-down for transformation Type Selector ", IconClass = "icon-menu")]
namespace GDI.Models.FormComponents
{
    
        public class HeroImageButtonComponent : FormComponent<HeroImageButtonProperties, string>
        {
            public const string IDENTIFIER = "HeroImageButtonComponent";

            [BindableProperty]
            public string? SelectedValue { get; set; }

            /// <summary>
            /// Get Hero Image Custom Layout list 
            /// </summary>
            /// <returns></returns>
            public IEnumerable<SelectListItem> GetIndexItems()
            {
                string buttonClass = ResHelper.GetString("Widget.HeroImage.ButtonClasses");
                string buttonoptions = ResHelper.GetString("Widget.HeroImage.ButtonOptions");
                var arr1 = buttonClass.Split(',');
                var arr2 = buttonoptions.Split(',');
                for (var i = 0; i < arr1.Length; i++)
                {
                    var listItem = new SelectListItem()
                    {
                        Value = arr1[i],
                        Text = arr2[i]
                    };
                    yield return listItem;
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


