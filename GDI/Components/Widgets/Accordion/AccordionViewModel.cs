using Kentico.Components.Web.Mvc.FormComponents;

namespace GDI.Components.Widgets.Accordion
{
    public class AccordionViewModel
    {
        /// <summary>
        /// IsVisible
        /// </summary>
        public bool? IsVisible { get; set; } = true;
        /// <summary>
        /// Description
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// GetAccordions
        /// </summary>
        public List<GDI.Business.Models.AccordionData>? GetAccordions { get; set; }
        /// <summary>
        /// GetAccordionTabs
        /// </summary>
        public List<GDI.Business.Models.AccordionTabData>? GetAccordionTabs { get; set; }
        /// <summary>
        /// Widget Path model
        /// </summary>
        public IList<PathSelectorItem>? Path { get; set; }
        /// <summary>
        /// Widget background Image
        /// </summary>
        public string? BackgroundImage { get; set; }
    }
}
