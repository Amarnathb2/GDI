using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;

namespace GDI.Models.FormComponents
{
    public class RepeaterPageTypeSelectorComponentProperties : FormComponentProperties<string>
    {

        public RepeaterPageTypeSelectorComponentProperties()
          : base(FieldDataType.Text)
        {
        }
        [DefaultValueEditingComponent("PageTypeSelectorComponentProperties", DefaultValue = "")]
        public override string DefaultValue { get; set; }
    }
}
