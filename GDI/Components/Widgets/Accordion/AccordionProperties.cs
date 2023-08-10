using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GDI.Components.Widgets.Accordion
{
    public class AccordionProperties : IWidgetProperties
    {
        public const string MEDIA_LIBRARY_NAME = "GDI";
        /// <summary>
        /// IsVisible
        /// </summary>
        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 0, Label = "Visible", Tooltip = "Indicates if the widget should be displayed.")]
        public bool? IsVisible { get; set; } = true;

        /// <summary>
        /// Description
        /// </summary>
        [EditingComponent(RichTextComponent.IDENTIFIER, Order = 2, Label = "Content")]
        public string? Content { get; set; }
        /// <summary>
        /// Please select the path
        /// </summary>
        [EditingComponent(PathSelector.IDENTIFIER, Label = "Please select the Path", Order = 3)]
        [EditingComponentProperty(nameof(PathSelectorProperties.RootPath), "/")]
        [Required(ErrorMessage = "Please Select Path")]
        public IList<PathSelectorItem>? Path { get; set; }
        /// <summary>
        /// Guid of Small image.
        /// </summary>
        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 4, Label = "Background Image*")]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.LibraryName), MEDIA_LIBRARY_NAME)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.AllowedExtensions), ".gif;.png;.jpg;.jpeg;.svg;.webp;")]
        [Required(ErrorMessage = "Please Select Default-Image")]

        public IEnumerable<MediaFilesSelectorItem>? BackgroundImage { get; set; } = new List<MediaFilesSelectorItem>();

    }
}
