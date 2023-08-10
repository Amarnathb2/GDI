using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace GDI.Components.Widgets.Card
{
    public class CardProperties : IWidgetProperties
    {
        public const string MEDIA_LIBRARY_NAME = "{$GDI.MediaLibrary.Name$}";

        /// <summary>
        /// Declaring the widget will visible or not
        /// </summary>
        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 0, Label = "Visible", Tooltip = "Set the visibility for widget to render")]
        public bool? IsVisible { get; set; } = true;
        /// <summary>
        /// Declaring to enter the video Title
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Title *", Tooltip = "Enter the Title.")]
        [EditingComponentProperty("Size", 200)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter Title.")]
        public string? Title { get; set; }
        /// <summary>
        /// Title URL
        /// </summary>
        [EditingComponent(UrlSelector.IDENTIFIER, Order = 2, Label = "Target Url *", Tooltip = "Enter Image Link Target.")]
        [EditingComponentProperty("Size", 200)]

        public string? TargetURL { get; set; }
        ///<summary>
        ///Declaring to enter Description
        /// </summary>
        [EditingComponent(TextAreaComponent.IDENTIFIER, Order = 3, Label = "Description *", Tooltip = "Enter the Description.")]
        [EditingComponentProperty("Size", 400)]

        public string? Description { get; set; }
        /// <summary>
        /// Dclaring to select Image
        /// </summary>
        [EditingComponent(MediaFilesSelector.IDENTIFIER, Label = "Image", Order = 4, Tooltip = "Select the Image.", ExplanationText = "Please select Only JPG or PNG files. Please upload the image of size not more than 100kb.")]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.LibraryName), MEDIA_LIBRARY_NAME)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.AllowedExtensions), ".gif;.png;.jpg;.jpeg;.svg;.webp;")]
        public IEnumerable<MediaFilesSelectorItem>? Image { get; set; }
        /// <summary>
        /// Declaring to enter the Alt Image Text
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 5, Label = "Image Alt Text", Tooltip = "Enter the Image Alt Text.")]
        [EditingComponentProperty("Size", 200)]

        public string? ImageAltText { get; set; }

        /// <summary>
        /// Declaring to select the Image Position
        /// </summary>
        [EditingComponent(DropDownComponent.IDENTIFIER, Order = 6, Label = "Image Position *", Tooltip = "Select the position of the Image.")]
        [EditingComponentProperty(nameof(DropDownProperties.DataSource), "{$GDI.CardWidget.ImagePosition$}")]
        [Required(ErrorMessage = "Please select value")]
        public string? ImagePosition { get; set; }
        /// <summary>
        /// Dclaring to select Background Image
        /// </summary>
        [EditingComponent(MediaFilesSelector.IDENTIFIER, Label = "Background Image", Order = 7, Tooltip = "Select the Image.", ExplanationText = "Please select Only JPG or PNG files. Please upload the image of size not more than 100kb.")]
        [VisibilityCondition(nameof(ImagePosition), ComparisonTypeEnum.IsEqualTo, "_background", StringComparison = StringComparison.OrdinalIgnoreCase)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.LibraryName), MEDIA_LIBRARY_NAME)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.AllowedExtensions), ".gif;.png;.jpg;.jpeg;.svg;.webp;")]
        public IEnumerable<MediaFilesSelectorItem>? BackgroundImage { get; set; }
        /// <summary>
        /// Dclaring to select Background Image
        /// </summary>
        [EditingComponent(MediaFilesSelector.IDENTIFIER, Label = "Background Small Image", Order = 8, Tooltip = "Select the Backgroun Small Image.", ExplanationText = "Please select Only JPG or PNG files. Please upload the image of size not more than 100kb.")]
        [VisibilityCondition(nameof(ImagePosition), ComparisonTypeEnum.IsEqualTo, "_background", StringComparison = StringComparison.OrdinalIgnoreCase)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.LibraryName), MEDIA_LIBRARY_NAME)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.AllowedExtensions), ".gif;.png;.jpg;.jpeg;.svg;.webp;")]
        public IEnumerable<MediaFilesSelectorItem>? BackgroundSmallImage { get; set; }
        /// <summary>
        /// Declaring to select the Background Color
        /// </summary>
        [EditingComponent(DropDownComponent.IDENTIFIER, Order = 9, Label = "Background", Tooltip = "Select the background Color.")]
        [EditingComponentProperty(nameof(DropDownProperties.DataSource), "{$GDI.CardWidget.BackgroundColor$}")]
        public string? Background { get; set; }
        /// <summary>
        /// Declaring the widget will visible or not
        /// </summary>
        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 10, Label = "Show Button", Tooltip = "Set the visibility for button to render")]
        public bool? IsButton { get; set; } = false;

        /// <summary>
        /// Declaring to select the Button Type
        /// </summary>
        [EditingComponent(DropDownComponent.IDENTIFIER, Order = 11, Label = "Button Type", Tooltip = "Select the Button Type.")]
        [VisibilityCondition(nameof(IsButton), ComparisonTypeEnum.IsTrue)]
        [EditingComponentProperty(nameof(DropDownProperties.DataSource), "{$GDI.ButtonComponent.ButtonStyle$}")]
        public string? ButtonType { get; set; }
        /// <summary>
        /// Declaring to enter the Button Name
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 12, Label = "Button Text", Tooltip = "Enter the Button Name.")]
        [EditingComponentProperty("Size", 200)]
        [VisibilityCondition(nameof(IsButton), ComparisonTypeEnum.IsTrue)]
        public string? ButtonTextOne { get; set; }

        /// <summary>
        /// Declaring to enter the Button Target
        /// </summary>
        [EditingComponent(DropDownComponent.IDENTIFIER, Order = 13, Label = "Button Target", Tooltip = "Enter the Button Target.")]
        [VisibilityCondition(nameof(IsButton), ComparisonTypeEnum.IsTrue)]
        [EditingComponentProperty(nameof(DropDownProperties.DataSource), "{$GDI.CardWidget.TargetURL$}")]
        public string? ButtonTargetOne { get; set; }
        /// <summary>
        /// Declaring to enter the Button URL
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 14, Label = "Button URL", Tooltip = "Enter the Button URL.")]
        [EditingComponentProperty("Size", 200)]
        [VisibilityCondition(nameof(IsButton), ComparisonTypeEnum.IsTrue)]
        public string? ButtonURL { get; set; }
        /// <summary>
        /// Declaring to enter the Button Background
        /// </summary>
        [EditingComponent(DropDownComponent.IDENTIFIER, Order = 15, Label = "Button Background", Tooltip = "Select the Button Background.")]
        [VisibilityCondition(nameof(IsButton), ComparisonTypeEnum.IsTrue)]
        [EditingComponentProperty(nameof(DropDownProperties.DataSource), "{$GDI.ButtonComponent.ButtonColor$}")]
        public string? ButtonBackground { get; set; }

    }

}
