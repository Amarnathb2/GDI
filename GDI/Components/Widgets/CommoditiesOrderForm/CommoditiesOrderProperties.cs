using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;

using System.ComponentModel.DataAnnotations;

namespace GDI.Components.Widgets.CommoditiesOrderForm
{
    public class CommoditiesOrderProperties : IWidgetProperties
    {
        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 1, Label = "Visible")]
        public bool? IsVisible { get; set; } = false;

        /// <summary>
        /// allow the editor to Select the Page Path to be displayed
        /// </summary>
        [Required(ErrorMessage = "Please Select path")]
        [EditingComponent(PathSelector.IDENTIFIER, Order = 2, Label = "Path")]
        [EditingComponentProperty(nameof(PathSelectorProperties.RootPath), "/")]
        public IList<PathSelectorItem>? Path { get; set; }
        /// <summary>
        /// Widget TopN Properties
        /// </summary>
        [EditingComponent(IntInputComponent.IDENTIFIER, Label = "TopN", Order = 3)]
        [Range(1, 4, ErrorMessage = "Please enter valid number")]
        public int? TopN { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 4, Label = "Product Cards Title")]
        [EditingComponentProperty("Size", 150)]
        public string? ProductCardTitle { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 5, Label = "Product Form Title")]
        [EditingComponentProperty("Size", 150)]
        public string? ProductFormTitle { get; set; }
        /// <summary>
        /// Declaring the RecordType  field
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 10, Label = "RecordType")]
        [EditingComponentProperty("Size", 100)]
        public string? RecordType { get; set; }
        /// <summary>
        /// Declaring the Origin  field
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 11, Label = "Origin")]
        [EditingComponentProperty("Size", 100)]
        public string? Origin { get; set; }
    }
}
