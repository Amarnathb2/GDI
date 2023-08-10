using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;

namespace GDI.Models.FormComponents
{
    public class RepeaterTransformationSelectorComponentProperties : FormComponentProperties<string>
    {

        public RepeaterTransformationSelectorComponentProperties()
          : base(FieldDataType.Text)
        {
        }
        [DefaultValueEditingComponent("PageTypeSelectorComponentProperties", DefaultValue = "")]
        public override string DefaultValue { get; set; }
    }
}
