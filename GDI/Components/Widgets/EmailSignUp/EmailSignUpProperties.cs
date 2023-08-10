using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GDI.Components.Widgets.EmailSignUp
{
    public class EmailSignUpProperties : IWidgetProperties
    {
        /// <summary>
        /// IsVisible
        /// </summary>
        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 0, Label = "Visible", Tooltip = "Indicates if the widget should be displayed.")]
        public bool? IsVisible { get; set; } = true;
        /// <summary>
        /// Declaring to enter the video Title
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Title ", Tooltip = "Enter the Title.")]
        [EditingComponentProperty("Size", 200)]
        public string? Title { get; set; }
        /// <summary>
        /// Content
        /// </summary>
        [EditingComponent(RichTextComponent.IDENTIFIER, Order = 2, Label = "Content")]
        public string? Content { get; set; }
        /// <summary>
        /// Please select the path
        /// </summary>
        [EditingComponent(PathSelector.IDENTIFIER, Label = "Please select the Path *", Order = 3)]
        [EditingComponentProperty(nameof(PathSelectorProperties.RootPath), "/")]
        [Required(ErrorMessage = "Please Select Path")]
        public IList<PathSelectorItem>? Path { get; set; }
    }
}
