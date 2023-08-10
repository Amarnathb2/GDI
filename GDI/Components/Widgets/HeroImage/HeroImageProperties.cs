using GDI.Models.FormComponents;
using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GDI.Components.Widgets.HeroImage
{
    public class HeroImageProperties : IWidgetProperties
    {
        public const string MEDIA_LIBRARY_NAME = "{$GDI.MediaLibrary.Name$}";

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 1, Label = "Visible")]
        public bool? Visible { get; set; } = true;

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 2, Label = "Hero Title")]
        [EditingComponentProperty("Size", 250)]
        //[Required(ErrorMessage = "Please Add Header Text")]
        public string? HeroTitle { get; set; }

        [EditingComponent(TextAreaComponent.IDENTIFIER, Order = 3, Label = "Hero Description", Tooltip = "Add Description", ExplanationText = "{$GDI.HeroImage.Description.ExplanationText$}")]
        [EditingComponentProperty("Size", 350)]
        [MaxLength(350)]
        public string? HeroDescription { get; set; }

        /// <summary>
        /// Guid of Small image.
        /// </summary>
        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 5, Label = "Hero Image*", ExplanationText = "{$GDI.HeroImage.DefaultImage.ExplanationText$}")]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.LibraryName), MEDIA_LIBRARY_NAME)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.AllowedExtensions), ".gif;.png;.jpg;.jpeg;.svg;.webp;")]
        [Required(ErrorMessage = "Please Select Default-Image")]

        public IEnumerable<MediaFilesSelectorItem>? DefaultImage { get; set; } = new List<MediaFilesSelectorItem>();

        /// <summary>
        /// Guid of Small image.
        /// </summary>
        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 6, Label = "Image - Small")]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.LibraryName), MEDIA_LIBRARY_NAME)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.AllowedExtensions), ".gif;.png;.jpg;.jpeg;.svg;.webp;")]
        [Required(ErrorMessage = "Please Select Image - Small")]
        public IEnumerable<MediaFilesSelectorItem>? ImageSmall { get; set; } = new List<MediaFilesSelectorItem>();

        /// <summary>
        /// Guid of Image Medium..
        /// </summary>
        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 8, Label = "Image - Medium")]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.LibraryName), MEDIA_LIBRARY_NAME)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.AllowedExtensions), ".gif;.png;.jpg;.jpeg;.svg;.webp;")]
        [Required(ErrorMessage = "Please Select Medium Image")]
        public IEnumerable<MediaFilesSelectorItem>? ImageMedium { get; set; } = new List<MediaFilesSelectorItem>();


        /// <summary>
        /// Guid of Image Large.
        /// </summary>
        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 10, Label = "Image - Large")]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.LibraryName), MEDIA_LIBRARY_NAME)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.AllowedExtensions), ".gif;.png;.jpg;.jpeg;.svg;.webp;")]
        [Required(ErrorMessage = "Please Select Large Image")]
        public IEnumerable<MediaFilesSelectorItem>? ImageLarge { get; set; } = new List<MediaFilesSelectorItem>();

        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 11, Label = "Image -X Large")]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.LibraryName), MEDIA_LIBRARY_NAME)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.AllowedExtensions), ".gif;.png;.jpg;.jpeg;.svg;.webp;")]
        [Required(ErrorMessage = "Please Select X-Large Image")]
        public IEnumerable<MediaFilesSelectorItem>? ImageXLarge { get; set; } = new List<MediaFilesSelectorItem>();

        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 12, Label = "Background-Image")]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.LibraryName), MEDIA_LIBRARY_NAME)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.AllowedExtensions), ".gif;.png;.jpg;.jpeg;.svg;.webp;")]
        [Required(ErrorMessage = "Please Select Background Image")]
        public IEnumerable<MediaFilesSelectorItem>? BackgroundImage { get; set; } = new List<MediaFilesSelectorItem>();

        [EditingComponent(DropDownComponent.IDENTIFIER, Order = 13, Label = "Select Page", Tooltip = "Select the Page-name you want to add the widget.")]
        [EditingComponentProperty(nameof(DropDownProperties.DataSource), "{$GDI.HeroWidget.CornerImage.SelectPageName$}")]
        public string? SelectPage { get; set; }

        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 14, Label = "Cross-Image-Left Top", ExplanationText = "{$GDI.HeroImage.CrossImageLeftTop.ExplanationText$}")]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.LibraryName), MEDIA_LIBRARY_NAME)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.AllowedExtensions), ".gif;.png;.jpg;.jpeg;.svg;.webp;")]
        [Required(ErrorMessage = "Please Select cross top Image")]
        public IEnumerable<MediaFilesSelectorItem>? CrossImageTop { get; set; } = new List<MediaFilesSelectorItem>();

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 15, Label = "Cross-Image-Top Alt Text")]
        [EditingComponentProperty("Size", 1000)]
        public string? CrossImageTopAltText { get; set; }

        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 16, Label = "Cross-Image-Right Bottom", ExplanationText = "{$GDI.HeroImage.CrossImageRightBottom.ExplanationText$}")]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.LibraryName), MEDIA_LIBRARY_NAME)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.AllowedExtensions), ".gif;.png;.jpg;.jpeg;.svg;.webp;")]
        [Required(ErrorMessage = "Please Select cross Bottom Image")]

        public IEnumerable<MediaFilesSelectorItem>? CrossImageBottom { get; set; } = new List<MediaFilesSelectorItem>();

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 17, Label = "Cross-Image-Bottom Alt Text")]
        [EditingComponentProperty("Size", 1000)]
        public string? crossImageBottomAltText { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 18, Label = "Button Text")]
        public string? ButtonTextOne { get; set; }
        [EditingComponent(UrlSelector.IDENTIFIER, Order = 19, Label = "Button URL")]
        public string? ButtonUrlOne { get; set; }

        [EditingComponent(DropDownComponent.IDENTIFIER, Label = "Button Target", Order = 20, Tooltip = "select the Target")]
        [EditingComponentProperty(nameof(DropDownProperties.DataSource), "_Self \r\n _Blank \r\n _parent\r\n _top \r\n ")]
        public string? ButtonTargetOne { get; set; }
        [EditingComponent(HeroImageButtonComponent.IDENTIFIER, Order = 21, Label = "Button Class*")]
        public string? ButtonClass { get; set; }

        [EditingComponent(HeroImageCustomLayoutSelector.IDENTIFIER, Order = 22, Label = "Transformation*")]
        public string? Transformation { get; set; }

        [EditingComponent(DropDownComponent.IDENTIFIER, Order = 23, Label = "Background Color", Tooltip = "Select the background Color.")]
        [VisibilityCondition(nameof(Transformation), ComparisonTypeEnum.IsEqualTo, "Corner", StringComparison = StringComparison.OrdinalIgnoreCase)]
        [EditingComponentProperty(nameof(DropDownProperties.DataSource), "{$GDI.HeroWidget.BackgroundColor$}")]
        public string? BackgroundColor { get; set; }


    }

}
