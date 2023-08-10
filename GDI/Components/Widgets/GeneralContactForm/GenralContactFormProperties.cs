using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
namespace GDI.Components.Widgets.GeneralContactForm
{
    public class GenralContactFormProperties : IWidgetProperties
    {

        /// <summary>
        /// Providing IsVisible Flexbility to Content Editor
        /// </summary>
        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 0, Label = "IsVisible")]
        public bool? IsVisible { get; set; } = false;
        /// <summary>
        /// Declaring the Title label to get the Title Information
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Title")]
        [EditingComponentProperty("Size", 100)]
        public string? Title { get; set; }
        /// <summary>
        /// Declaring the Subtitle label to get the Title Information
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 2, Label = "Subtitle")]
        [EditingComponentProperty("Size", 100)]
        public string? Subtitle { get; set; }
        /// <summary>
        /// Declaring the RecordType  field
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 3, Label = "Record Type ID")]
        [EditingComponentProperty("Size", 100)]
        public string? RecordType { get; set; }
        /// <summary>
        /// Declaring the Origin  field
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 4, Label = "Origin")]
        [EditingComponentProperty("Size", 100)]
        public string? Origin { get; set; }
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 5, Label = "Tags")]
        [EditingComponentProperty("Size", 100)]
        public string? Tags { get; set; }
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 6, Label = "External Campaign Id")]
        [EditingComponentProperty("Size", 100)]
        public string? ExternalCampaignId { get; set; }
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 7, Label = "Type")]
        [EditingComponentProperty("Size", 100)]
        public string? Type { get; set; }
    }
}
