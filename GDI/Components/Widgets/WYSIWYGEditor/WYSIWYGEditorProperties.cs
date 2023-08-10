using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;

namespace GDI.Components.Widgets.WYSIWYGEditor
{
    public class WYSIWYGEditorProperties : IWidgetProperties
    {
        /// <summary>
        /// Declaring the widget will visible or not
        /// </summary>
        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 0, Label = "Visible", Tooltip = "Set the visibility for widget to render")]
        public bool Visible { get; set; } = true;
        /// <summary>
        /// Html Text
        /// </summary>
        public string? HtmlText { get; set; }
    }
}

