using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;

namespace GDI.Models.FormComponents
{
    public class HeroImageCustomLayoutSelectorProperties : FormComponentProperties<string>
    {

        public HeroImageCustomLayoutSelectorProperties()
             : base(FieldDataType.Text)
        {
        }
        [DefaultValueEditingComponent("HeroImageCustomProperties", DefaultValue = "")]
        public override string DefaultValue { get; set; }

    }
}
