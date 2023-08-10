using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;

namespace GDI.Models.FormComponents
{
    public class HeroImageButtonProperties : FormComponentProperties<string>
    {


        public HeroImageButtonProperties()
                   : base(FieldDataType.Text)
        {
        }
        [DefaultValueEditingComponent("HeroImageButtonProperties", DefaultValue = "")]
        public override string DefaultValue { get; set; }
    }
}
